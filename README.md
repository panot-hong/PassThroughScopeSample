# Pass Through Data Over IServiceProvider.CreateScope() sample
In some cases you may encounter the situation that you need to pass through some particular data over a new scope of Service Provider.

For instance, when you implement a solution that integrate to a third party that has webhook callbacks. There might be a data from the webhook request that is necessary to be resolved by each service function.

![Flow](https://cdn-images-1.medium.com/max/800/1*1Hx5SJk1EmI2D16DAVb8sg.png)

The payload of the request contains identity like tenant id. API Controller received the request then it make a request to the services. Most cases we can carry the payload over to the service request via function parameter but for more complex project structure there might be a middleware, filters, service calling another service, attribute, etc. where they should be able to resolve the identifier (like tenant id in this sample) via either a normal dependency injection way or `IServiceProvider.GetService<TenantIdentifier>()`.

This might be a rare use case but I post this for anyone who already encounter the same situation and my technique may offer you one more workaround option.

Full detail can be found at my [medium post](https://medium.com/@panott/pass-through-data-over-iserviceprovider-createscope-77cc9a12b3a6).