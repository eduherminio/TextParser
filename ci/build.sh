#!/usr/bin/env bash

# To be invoked from outside ci folder

# exit if any command fails
set -e

dotnet restore ./src
dotnet build ./src/FileParser --configuration Release --force --no-incremental --framework netstandard2.1

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "%04d" $revision)