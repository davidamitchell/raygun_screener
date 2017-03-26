# Raygun Screener

## Requirements:
* Pass the user to Trello for auth and then allow for subsequent entry of the associated Trello token from the user
* Provide an effective UI for selection of a card from a board that the user has access to
* Allow the user to supply a comment which will then be applied to the card
* Ensure that the system gracefully handles any issues

## Prerequisites
* using nuget for package management
* RestSharp -- used for the Rest api remote calls
  * from the nuget console: `Install-Package RestSharp`
* Unity.Mvc5 -- used for Dependency Injection
  * from the nuget console: `install-package Unity.Mvc5`
* envronment varables (see below)

## Environment settings
To manage the developer api key (gotten from https://trello.com/app-key) and the application name two environment variables need to be set.  If these are not set the application will fail to start.
* TRELLO_APP_NAME -- the application name
  * in windows powershell: 
 
```[Environment]::SetEnvironmentVariable("TRELLO_APP_NAME", "Some application name", "Machine")```
* TRELLO_DEV_KEY -- the developer api key
  * in windows powershell: 
  
```[Environment]::SetEnvironmentVariable("TRELLO_DEV_KEY", "xyz", "Machine")```

## Improvements
There are a couple of places where I see the need for improvement.  Currently the error handling is quite rudimentary.  If there is an error an error page is return.  If no results are found a simple "No results found" message is returned.  

### Check for a valid token
Currently if an invalid token is passed to the application a simple "No results found" for the boards list is returned.  This could be improved by validating the token and returning a more descriptive error message, on all controllers and routes, if the token is invalid.

### Check for timeouts and network problems
As in the case of the invalid token a "No results found" message is returned if there is a timeout or a network connection problem.  Better error handling would look at the response from RestSharp and/or the `ErrorException` value in the response.  Something like what is done: https://github.com/kamranayub/GiantBomb-CSharp/blob/master/GiantBomb.Api/Core.cs is the recommended way of handling errors returned from RestSharp.

### Startup configuration excpetion type
Currently if the configuration is invalid a `ConfigurationException` is raised.  This excpetion type is currently deprecated and a custome exception should be used instead.

### Dev, test, prod environments
Different configuration, error pages etc. for each type of different runtime environments.

### Test test and more tests
Missing any type of unit and front end tests

### Better logging
Or any for that matter.  Should have different levels of loggging for the different runtime environments.

### Use configuration file for `TrelloApiConfig` settings
The `TrelloApiConfig` static class hold the configuration settings like the `baseUri` and the Trello api version.  This data could be stored in configuration files.  Could have a different one per environment which would allow something like a user acceptance environment to call a stubed api instead of the real Trello api.
