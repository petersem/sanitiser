# sanitiser
Quick way to sanitise environment files for Docker

takes a given file name and searches each variables that end in `__SECRET`. If found, then anything after an '=' sign will be sanitised.

for example: unsanitised .env file

    S_API_KEY=abc123efg456
    SOMEVALUE=true
    B_API_KEY=abc123efg456

If you wanted to publish this with the secrets removed, the add a `__SECRET` to the variable names you need sanitised

    S_API_KEY__SECRET=abc123efg456
    SOMEVALUE=true
    B_API_KEY__SECRET=abc123efg456

Then run 'sanitiser .env'

A new file will be created called '.env(sanitised)', with the following content

    S_API_KEY__SECRET=<enter your value>
    SOMEVALUE=true
    B_API_KEY__SECRET=<enter your value>
