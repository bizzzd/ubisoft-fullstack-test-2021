# ubisoft-fullstack-test-2021
The app created during one of interview steps with Ubisoft Dusseldorf. 


##Here is the original text of test task:
  _   _       _              __        __   _       _____         _
 | | | |_ __ | | __ _ _   _  \ \      / /__| |__   |_   _|__  ___| |_
 | | | | '_ \| |/ _` | | | |  \ \ /\ / / _ \ '_ \    | |/ _ \/ __| __|
 | |_| | |_) | | (_| | |_| |   \ V  V /  __/ |_) |   | |  __/\__ \ |_
  \___/| .__/|_|\__,_|\__, |    \_/\_/ \___|_.__/    |_|\___||___/\__|
       |_|            |___/                             Uplay Web Test


Welcome to your new job at Acme Games. You have been hired to make a web site
where users can log in to view their account information and their games.

The system handles user accounts, games and ownership of games. It also allows
users to redeem keys to get ownership of a game.

A user account is an entity with the following information:
- User account id
- First name
- Last name
- E-mail address
- Password
- Date of birth
- Is admin

An ownership is an entity that connects a user account with a game and has the
following information:
- Ownership id
- User account id
- Game id
- State (indicates owned/revoked)
- Registered date

A game is an entity with the following information:
- Game id
- Name
- Thumbnail
- Age restriction

A key is an entity with the following information:
- Key
- Game id
- Is redeemed

Your task is to implement a single page application where users can:

1. Log in using their e-mail address and password. Log in is mandatory and a
user should only be allowed to view their own account.
2. When logged in they should see their account information. They should also be
able to update:
	- E-mail address
	- First name
	- Last name
	- Password
3. The user should also see a list of all their owned games.

You need to provide the front end implementation and an API that transmits,
modifies and persists data.

NOTE: It is fine for the purposes of the test to keep all data in memory.

Please send us back your source code together with a report describing your 
thought process and how you tackled the problem.

If you have time, please consider the following bonus assignments:
* Users should be able to redeem a key which grants them ownership of a game if
the key has not been previously redeemed.
* Support for administration of user accounts. If the logged in user is flagged
as an admin, they should be able to:
	- List all user accounts.
	- Search for a specific user account.
	- View and edit all details of a user account.
	- Grant a user ownership of a game, without the use of a key.
	- Revoke a user's ownership of a game.

The application should run in evergreen browsers (Chrome, Firefox, Edge).
The back end should be written in ASP.NET (Core or Framework). We've included
a basic project with some boilerplate code to get you started. You can modify
the starter code if you so choose.

You can use Visual Studio 2017 Community edition or Visual Studio Code
to load the project.

Community edition available here: https://www.visualstudio.com/downloads/
Visual Studio Code is available here: https://code.visualstudio.com/

Have fun and good luck!

/ The Uplay Team
	* Massive Entertainment
	* Ubisoft Kiev
	* Blue Byte
