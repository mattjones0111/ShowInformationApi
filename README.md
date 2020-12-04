# ShowInformationApi

A REST API that exposes information about TV shows, and retrieves that information
from the TvMaze API.

## Dependencies

- Instance of SQL Server (I have used localdb).
- dotnet core 3.1

## Usage

- Clone the repo.
- Create a new SQL database in SQL server, ensure the connection string is 
set in appsettings.json in the Api project.
- Run `dotnet run` in \src\Api.

## Development notes

- This took a little longer than the given time as I did it over a few days
while switching contexts.
- I used netcore3.1 rather than 2.2; I hope this is ok.
- I implemented a simple, home-spun SQL document store to store each Show as a document.
This is not optimal; it would have been preferable to use an established document db
like CosmosDb, MongoDb etc. I thought that for the given requirement, mapping the
documents to and from a tabular db was excessive.
- For the purposes of this exercise, I placed much of the code in the Api project
however the ingestion service, contracts and the infrastructure adapters would be moved
out to other packages.
- I am not happy with how the ingestor service works; it retrieves all the source
and their cast information data before returning/persisting. With more time I
would change it so that it did these one at a time.
- I kept unit tests to a minimum and mainly focussed on integration tests.
- It was/is always difficult to get the balance right between 'just enough'
and 'too much' and I was unsure how deep to go with this.
- Finally, and most importantly, feedback/suggestions are always welcome. :-)
