@echo off

echo -------------------------------------------------------------------------------
echo PACKING Facebook.Core Release
echo -------------------------------------------------------------------------------


cd ../src/Facebook.Core
dotnet pack --output ../../builds --configuration Release

IF NOT "%1"=="-noPause" pause