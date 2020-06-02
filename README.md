# About
This is just a small test for an asp.net web api creating an excel spreadsheet using the OpenXML framework. The file content is a table outlining the binary linkage for multiplications in modulo 10 residue field.

# Building
```sh
# build the solution (.NET core) and apply the binaries to a docker image
docker build -t "openxml-test:1.0" .
```

# Testing (e.g. Ubuntu 18.04 with LibreOffice pre-installed)
```sh
# install curl (only if necessary): 
sudo apt-get update && sudo apt-get install curl

# define variables
export PORT=80
export TEMP_FILE=temp.xlsx
export CONTAINER_NAME=openxml-test

# create a new container running the webservice
docker run -d -p $PORT:80 --name $CONTAINER_NAME openxml-test:1.0

# request a file from the webservice and store it locally as 'test.xlsx'
curl http://localhost:$PORT/documents --output $TEMP_FILE

# open the document with libreoffice (a warning may show up, ignore it anyways)
libreoffice $TEMP_FILE

# stop and delete container
docker stop $CONTAINER_NAME && docker container rm $CONTAINER_NAME
```
