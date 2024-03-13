### Auth

> pac auth create // creer une connexion
> pac auth list // lister les ref de connexion
> pacx auth select --index 1 // choisir une connexion

### Organisation
> pac org list // liste les environnements

### Plugin

> pac plugin init -o name

### Model Builder

pac modelbuilder build --outdirectory . --settingsTemplateFile modelbuilder.json --environment {envGuid}

### Solution

>  pac solution export -env guid -n name -p c:\path.zip -ow -i customization,general

others values
* autonumbering
* calendar
* emailtracking
* externalapplications
* isvconfig
* marketing
* outlooksynchronization
* relationshiproles
* sales

> pac solution import -env guid -p c:\path.zip -pc -slv -up  
> pac solution init --publisher-name developer --publisher-prefix dev

### PCF
> pac pcf init --namespace SampleNameSpace -n SampleComponent -t field -fw react -npm
