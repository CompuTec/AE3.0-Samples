# Jobs
Jobs is a task that executes in appengine. the execution of a job can be triuggered by Cron excpression , sap1 event or using rest APi.In the app engine configuration job can be assigned to a company.The job can have a connection (sap ) to the custommer database ,db connection (ADO) to the custommer database, or no connection.
The main goal is to automate some processys in sap b1 plugins.

## Event based Job
This type of event can react on the SAP B1 Event that occues in the company.
Standard sapb1 message informations:
   * ActionType -A,U,C,D
   * ContentType- sap object type
   * Key - key 
   * KeyName- key name 
   * UseCode-user code 
   * UserId- UserId

For this type of Job you must specify EventBusJob
```csharp
[EventBusJob(JobId ="JobId",Description ="JobName",ActionType = "*",ContentType = "CT_VO_OVMD")]
```
there are tree base classes your class must implement :
* **EventBusDatabaseJob** in this case the exeution does not have any depenencies related with the sap connectoon and you can only use          IDbConnection DBConnection property for your custom logic, altho you can inject Applciation Scoped services if you have to resolved via constructor.
* **EventBusJob**  in this type of job you dont have any connection you can only use the applciation scope services resolved via constructor
* **EventBusSecureJob** in this type of you have full connection to the database. You can resolve connecion based services via GetService<T> Function 

## One Time And Recursive based Jobs
the one time jobs executes on the demand via rest api 
Recursive job are executing periodically with the cron expression for the cron expression please use this generator https://www.freeformatter.com/cron-expression-generator-quartz.html

there are two classes uou can use to write the job:
* **Job** in this type of job you dont have any connection you can only use the applciation scope services resolved via constructor
*  **SecureJob** in this type of you have full connection to the database. You can resolve connecion based services via GetService<T> Function 

to deteremin if this is job is recursive or one time please add apropriate attribuete you can mix them 

```csharp
[BackgroundJob(JobId = "AE_Plugin_ScheduledJob3AsOneTime", JobName = "AE_Plugin_ScheduledJob3AsOneTime" )]
[RecurringJob(JobId = "AE_Plugin_ScheduledJob3", JobName = "BacgroundJob3", CronExpression = "0/20 0 0 ? * * *"
)]
```