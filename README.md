LazyLayer is an idea of generic and reusable Request/Response pattern implementation.

Philosophy behind it is that consumer of the layer should provide only 2 things:
 1. Method that does a conversion of LazyLayer responses into some other specific response like IHttpActionResult of WebAPI.
 2. Method that needs to be invoked, e.g.GetUser.

What LazyLayer offers:
 1. Means to inject your favorite logging provider. (LazyLayer.Serilog is an example of Serilog implementation)
 2. Means to inject data validation easily.
 3. Exception handling - LazyLayer will catch any failure and return an error response with data.
 4. Basic performance measuring - it collects execution times of every request it processes.
 You can find an example for WebAPI under LazyLayer.Example.WebApi.
 5. LazyLayer.Http - provides setup for WebAPI all that is required is to inherit LazyController.

 I will add proper documentation and main idea in the future, currently I really don't have enough time, 
 but I am really interested in your opinion and feedback.


 Thanks,
 Mirza.
