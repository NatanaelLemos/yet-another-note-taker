# yet another note taker

## A note taker app
----


### Dependencies
- Docker runtime
- .Net Core 3.1
- MongoDB
- Xamarin Forms

### TODOs
- Testing
- Xamarin local database
- WebAssembly dark theme
- Code cleanup


### PORTS
- YetAnotherNoteTaker.Server
    - Local: 5000
    - Docker/Development: 5000 / webserver
- MongoDb
    - Local: 5002
    - Docker/Development: 5002 / mongodb
- YetAnotherNoteTaker.Blazor (WebAssembly / Docker / Nginx)
    - Local: 5004
    - Docker/Development: 5003