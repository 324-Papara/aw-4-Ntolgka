using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.RabbitMQ;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class AccountCommandHandler :
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<UpdateAccountCommand, ApiResponse>,
    IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly RabbitMQClient rabbitMqClient;

    public AccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, RabbitMQClient rabbitMqClient)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.rabbitMqClient = rabbitMqClient;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountRequest, Account>(request.Request);
        mapped.OpenDate = DateTime.UtcNow;
        mapped.Balance = 0;
        mapped.AccountNumber = new Random().Next(1000000, 9999999);
        mapped.IBAN = $"TR{mapped.AccountNumber}97925786{mapped.AccountNumber}01";
        var saved = await unitOfWork.AccountRepository.Insert(mapped);
        await unitOfWork.Complete();

        var customer = await unitOfWork.CustomerRepository.GetById(request.Request.CustomerId);

        var emailMessage = new EmailMessage
        {
            Subject = "New account registered!",
            Email = customer.Email,
            Content = $"Hello, {customer.FirstName} {customer.LastName}, your account in {request.Request.CurrencyCode} currency has been created."
        };

        // Publish to RabbitMQ
        rabbitMqClient.PublishEmailMessage(emailMessage);

        var response = mapper.Map<AccountResponse>(saved);
        return new ApiResponse<AccountResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountRequest, Account>(request.Request);
        mapped.Id = request.AccountId;
        unitOfWork.AccountRepository.Update(mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AccountRepository.Delete(request.AccountId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}