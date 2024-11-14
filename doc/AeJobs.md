
# Jobs in AppEngine

Jobs are tasks that execute within the AppEngine environment, designed to automate processes within SAP B1 plugins. Jobs can be triggered by a Cron expression, SAP B1 event, or REST API. They may connect to a company's SAP database, a customer database (via ADO), or operate without a connection.

## Event-Based Jobs

Event-based jobs react to specific SAP B1 events that occur in the company. Standard SAP B1 message information includes:

- **ActionType**: A, U, C, D
- **ContentType**: SAP object type
- **Key**: Key value
- **KeyName**: Key name
- **UseCode**: User code
- **UserId**: User ID

For this type of job, use the `EventBusJob` attribute:

```csharp
[EventBusJob(JobId = "JobId", Description = "JobName", ActionType = "*", ContentType = "CT_VO_OVMD")]
```

There are three base classes your job class must implement:

- **EventBusDatabaseJob**: Executes with no SAP connection, but you can use the `IDbConnection DBConnection` property for custom logic. Application-scoped services are available via constructor injection.
- **EventBusJob**: Executes without any connection, relying only on application-scoped services resolved via constructor.
- **EventBusSecureJob**: Provides a full connection to the database, allowing access to connection-based services via `GetService<T>()`.

## One-Time and Recursive Jobs

- **One-Time Jobs**: Execute on demand via the REST API.
- **Recursive Jobs**: Execute periodically according to a Cron expression (see [Quartz Cron Generator](https://www.freeformatter.com/cron-expression-generator-quartz.html)).

Classes for creating these jobs:

- **Job**: No database connection, can only use application-scoped services resolved via constructor.
- **SecureJob**: Full connection to the database; connection-based services can be accessed via `GetService<T>()`.

To configure the job as one-time or recursive, add the appropriate attribute:

```csharp
[BackgroundJob(JobId = "AE_Plugin_ScheduledJob3AsOneTime", JobName = "AE_Plugin_ScheduledJob3AsOneTime")]
[RecurringJob(JobId = "AE_Plugin_ScheduledJob3", JobName = "BackgroundJob3", CronExpression = "0/20 0 0 ? * * *")]
```

## Job Configuration

You can add configuration to `Secure` and `Database` type jobs by using the `AdditionalJobConfiguration` attribute with a specified configuration type. In your job class, access the configuration by using the `Configuration` property. For more details on configurations, see [Configurations](Configurations.md).

```csharp
[AdditionalJobConfiguration(Type = typeof(Adv))]
public class ScheduledJob2 : EventBusSecureJob
{
    public void ExecuteJob()
    {
        var config = Configuration; // Access configuration here
        // Job logic
    }
}
```
