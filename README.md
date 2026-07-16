# Manage Your Education and Skills Funding Contracts Feed Processor Function
The Manage Your Education and Skills Funding Contracts Feed Processor Function app is used by the MYESF to allow the following:
- Reads FCS ATOM feed.
- Process the feed and add message to service bus message queue.

## Provider

[The Department for Education](https://www.gov.uk/government/organisations/department-for-education)

## About this project

This project is a .Net Core 3.1 timer triggered Azure Function project utilizing an Azure Function App for deployment.

**Note:** The project is currently being updated to be containerised via Docker where the deployment method and target will change, this document will be updated when these changes have been finalised.

# Local Configuration Guide

For running the application locally, `local.settings.json` file need to be created in the `Pds.Contracts.FeedProcessor.Func` project. Below, and included in the repo, there is `local.settings.example.json` which can be used as a base and populated with the required values, which can be retrieved from the Azure Portal.

The Microsoft Azure Storage Emulator can be used to emulate the Azure Blob, Queue, and Table services for local development purposes.

## Application Settings (`local.settings.json`)
```
{
  "IsEncrypted": false,
  "version": "2.0",
  "Values": {
    "APPINSIGHTS_INSTRUMENTATIONKEY": ""
    "AuditApiConfiguration": {
      "ApiBaseAddress": "",
      "AppUri": "",
      "Authority": "",
      "ClientId": "",
      "ClientSecret": "",
      "ShouldSkipAuthentication": "",
      "TenantId": ""
    },
    "AzureStorageAccountOptions": {
      "BlobAccessOptions": {
        "Delay": "0.00:00:15",
        "RetryCount": "3",
        "XmlStorageContainer": ""
      },
      "ConnectionString": "",
      "TableAccessOptions": {
        "ConfigTableName": "",
        "DeltaBackOff": "0.00:00:15",
        "MaxAttempts": "3",
        "PartitionKey": "FeedProcessor"
      }
    },
    "AzureWebJobsStorage": "",
    "ContractEventsSessionQueue": "",
    "Environment": "at",
    "FeedReaderOptions": {
      "ApiBaseAddress": "",
      "Authority": "",
      "ClientId": "",
      "ClientSecret": "",
      "TenantId": "",
      "HttpPolicyOptions": {
        "CircuitBreakerDurationOfBreak": "0.00:00:15",
        "CircuitBreakerToleranceCount": "5",
        "HttpRetryBackoffPower": "2",
        "HttpRetryCount": "3"
      }
    },
    "FUNCTIONS_EXTENSION_VERSION": "~3",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "PdsApplicationInsights": {
      "Environment": "",
      "InstrumentationKey": ""
    },
    "SchemaValidationSettings": {
      "EnableSchemaVersionValidation": "false",
      "SchemaManifestFilename": "contract_corporate_schema_v11.09.xsd",
      "SchemaVersion": "11_09"
    },
    "TimerInterval": "*/30 * * * * *"
  }
}
```
### Setting Details

- **`APPINSIGHTS_INSTRUMENTATIONKEY`**  
  The key value for Application Insights resource for logging.

- **`AuditApiConfiguration:ApiBaseAddress`**  
  The base URL endpoint for Audit API.

- **`AuditApiConfiguration:AppUri`**  
  The unique Application ID URI used as the identifier for the protected Audit API resource within the Identity Provider.

- **`AuditApiConfiguration:Authority`**  
  The base URL of the Identity Provider responsible for authenticating and issuing tokens for the Audit API client.

- **`AuditApiConfiguration:ClientId`**  
  The unique identifier assigned to the admin client application to authenticate its identity against the security provider when calling the Audit API.

- **`AuditApiConfiguration:ClientSecret`**  
  The secret credential used by the Audit client application to securely prove its identity to the Identity Provider.

- **`AuditApiConfiguration:TenantId`**  
  The unique identifier that specifies the exact organization or cloud instance within the Identity Provider where the Audit API client is registered.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:BlobAccessOptions:Delay`**  
  The duration to wait between retry attempts when a transient error occurs during a Blob storage operation.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:BlobAccessOptions:RetryCount`**  
  The maximum number of retry attempts allowed for a single Blob storage operation before throwing an exception.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:BlobAccessOptions:XmlStorageContainer`**  
  The name of the specific Azure Blob Storage container where XML data or configuration files are stored.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:ConnectionString`**  
  The primary authentication connection string containing endpoints and credentials used to connect to the custom Azure Storage Account.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:TableAccessOptions:ConfigTableName`**  
  The name of the Azure Table Storage table designated to hold application configuration settings or state data.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:TableAccessOptions:DeltaBackOff`**  
  The incremental time value used in exponential backoff algorithms to increase the wait time between subsequent table operation retries.

- **`AzureStorageAccountOptions:AzureStorageAccountOptions:TableAccessOptions:MaxAttempts`**  
  The maximum number of connection or execution attempts permitted for an Azure Table Storage operation before it fails.

- **`AzureStorageAccountOptions:TableAccessOptions:PartitionKey`**  
  The logical partition key identifier used to group related entities inside Azure Table Storage for optimal querying.

- **`AzureWebJobsStorage`**  
  The standard environment variable used by the Azure Functions host to manage essential runtime operations like logging, triggers, and locks.

- **`ContractEventsSessionQueue`**  
  Azure service bus queue name.

- **`Environment`**  
  The environment which the app is running on.

- **`FeedReaderOptions:ApiBaseAddress`**  
  The base URL endpoint for Feed Reader API.

- **`FeedReaderOptions:AppUri`**  
  The unique Application ID URI used as the identifier for the protected Feed Reader API resource within the Identity Provider.

- **`FeedReaderOptions:Authority`**  
  The base URL of the Identity Provider responsible for authenticating and issuing tokens for the Feed Reader API client.

- **`FeedReaderOptions:ClientId`**  
  The unique identifier assigned to the admin client application to authenticate its identity against the security provider when calling the Feed Reader API.

- **`FeedReaderOptions:ClientSecret`**  
  The secret credential used by the Feed Reader client application to securely prove its identity to the Identity Provider.

- **`FeedReaderOptions:TenantId`**  
  The unique identifier that specifies the exact organization or cloud instance within the Identity Provider where the Feed Reader API client is registered.

- **`FeedReaderOptions:FcsAtomFeedSelfPageEndpoint`**  
  Latest page path for Feed Reader API

- **`FeedReaderOptions:HttpPolicyOptions:CircuitBreakerDurationOfBreak`**  
  The duration (typically a `TimeSpan` string) that the circuit breaker remains open, blocking all outgoing HTTP requests, before entering a test state.

- **`FeedReaderOptions:HttpPolicyOptions:CircuitBreakerToleranceCount`**  
  The consecutive number of failed HTTP requests or specific status codes allowed before the circuit breaker trips and opens.

- **`FeedReaderOptions:HttpPolicyOptions:HttpRetryBackoffPower`**  
  The mathematical exponent or base value used to calculate the exponential delay between consecutive HTTP retry attempts.

- **`FeedReaderOptions:HttpPolicyOptions:HttpRetryCount`**  
  The maximum number of retry attempts allowed for a single HTTP request when encountering transient network errors or specific failure status codes.

- **`FUNCTIONS_EXTENSION_VERSION`**  
  The functions extension version number.

- **`FUNCTIONS_WORKER_RUNTIME`**  
  The functions runtime.

- **`PdsApplicationInsights:Environment`**  
  The environment which the app is running on for Application Insights for logging purposes.

- **`PdsApplicationInsights:InstrumentationKey`**  
  The key for Application Insights resource for logging purposes.

- **`SchemaValidationSettings:EnableSchemaVersionValidation`**  
  Boolean value for enabling enable schema version validation.

- **`SchemaValidationSettings:SchemaManifestFilename`**  
  Schema file name.

- **`SchemaValidationSettings:SchemaVersion`**  
  Schema Version

- **`TimerInterval`**  
  A CRON expression string (e.g., `0 */5 * * * *`) that defines the schedule or execution interval for a background worker, recurring job, or Azure Function timer trigger.
