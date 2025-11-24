# Week 13: Diagnostics

This week’s project was diagnostics. The requirements were to add a /healthz endpoint that returns clear status, include at least one dependency check (for example, database connectivity), and return enough detail to troubleshoot without exposing secrets. I did this by creating a custom DbHealthCheck class, which implements the IHealthCheck interface. This class contains two functions: CheckHealthAsync and WriteResponse. CheckHealthAsync is pretty simple. It first checks if the database is reachable using CanConnectAsync(). If that is true, then CheckHealthAsync returns a healthy HealthCheckResult with the description “Database connection is OK.” If not, it returns an unhealthy HealthCheckResult with the description “Cannot connect to the database.” The descriptions are intentionally general so that the endpoint does not reveal sensitive information. I then implemented a JSON response writer by following Microsoft's health check tutorial, which I tweaked a little for clarity. The response writer returns a JSON response that includes the overall health status as well as the result of each individual check. This makes it clear whether the system is operating normally or experiencing issues with the database. I tested it by running the application with the database online and then again with the database connection string intentionally incorrect. In both cases, the endpoint produced accurate and safe diagnostic output.

These are screenshots of the responses:

## Healthy
<img width="1599" height="945" alt="Healthy" src="https://github.com/user-attachments/assets/a363e156-0575-4e78-a2ae-34e8a9b05fe5" />

## Unhealthy
<img width="1493" height="816" alt="Unhealthy" src="https://github.com/user-attachments/assets/1a845f5a-5c63-40e0-a724-6b3827ee525c" />
