# sanitiser
Quick way to sanitise environment files for Docker

takes a given file name and searches each line for '*secret*'. If found, then line will be sanitised for anything after an '=' sign.

for example: unsanitised .env file

S_API_KEY=abc123efg456
SOMEVALUE=true
B_API_KEY=abc123efg456

If you wanted to publish this with the secrets removed, the add a # *secret* comment above any line you need sanitised

# *secret*
S_API_KEY=abc123efg456
SOMEVALUE=true
# *secret*
B_API_KEY=abc123efg456

Then run 'sanitiser .env'

A new file will be created called '.env(sanitised)', with the following content

S_API_KEY=<enter your value>
SOMEVALUE=true
B_API_KEY=<enter your value>
