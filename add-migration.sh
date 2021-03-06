echo Creating migrations named $1

cp ./src/NoteTaker.Data/NoteTakerContext.cs ./src/NoteTaker.Data.MigrationsGenerator/NoteTakerContext.cs

cd ./src/NoteTaker.Data.MigrationsGenerator/

dotnet restore
dotnet ef migrations add $1

rm ./NoteTakerContext.cs