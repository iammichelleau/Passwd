# Passwd as a Service

Passwd is a HTTP service that exposes the user and group information on a UNIX-like system that is usually locked away in the UNIX `/etc/passwd` and `/etc/groups` files.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

#### Technologies
1. C#
    Visual Studio - https://visualstudio.microsoft.com/vs/
2. Node.js - https://nodejs.org/en/
3. Angular - https://angular.io/guide/quickstart

### Running the Application

- Clone the repo https://github.com/iammichelleau/Passwd.git. 
- Running the UI
    - Change directory to:
        `PasswdUI/passwd-app`.
    - Run the following command: 
        `ng serve --o`.
- Running the API
    - Change directory to:
        `PasswdApi`.
    - Open the Visual Studio solution `PasswdApi.sln`.
    - Start the API with Visual Studio.

### Using the Application

#### Configuration Service

- Enter a valid file path to the `/etc/passwd` and `/etc/groups` files. 
    - UNIX machines - these files already exist. 
    - Windows - create files or use the sample files in the `etc` dirctory of this repo. 
- Click `Submit` button to save the confirguations. 

#### User Service

- `/users` - get a list of the users in the `/etc/passwd` file.
    - E.g. http://localhost:41177/api/users
- `/users/{uid}` - get a user by `uid`.
    - E.g. http://localhost:41177/api/users/0
- `/users/query{name}{uid}{gid}{comment}{home}{shell}` - get user(s) by query. 
    - Each of the parameters is nullable.
    - E.g. http://localhost:41177/api/users/query?name=root&uid=0
- `/users/{uid}/groups` - get the groups of a user by `uid`.
    - E.g. http://localhost:41177/api/users/0/groups

#### Group Service

- `/groups` - get a list of the groups in the `/etc/groups` file.
    - E.g. http://localhost:41177/api/groups
- `/groups/{gid}` - get a group by `gid`.
    - E.g. http://localhost:41177/api/groups/0
- `/groups/query{name}{gid}{member}` - get group(s) by query. 
    - Each of the parameters is nullable.
    - The `member` parameter could be repeated.
    - E.g. http://localhost:41177/api/groups/query?name=wheel&member=root&member=nobody