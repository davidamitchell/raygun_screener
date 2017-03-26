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

## Envronment settings
To manage the developer api key (gotten from https://trello.com/app-key) and the application name two evnronment variables need to be set.  If these are not set the application will fail to start.
* TRELLO_APP_NAME -- the application name
 * in windows powershell: `[Environment]::SetEnvironmentVariable("TRELLO_APP_NAME", "Some application name", "Machine")`
* TRELLO_DEV_KEY -- the developer api key
 * in windows powershell: `[Environment]::SetEnvironmentVariable("TRELLO_DEV_KEY", "xyz", "Machine")`
