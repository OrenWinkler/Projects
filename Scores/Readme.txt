Solution structure:

Feeds:
	LiveScoreFeed: 
		Stateless Azure Timer Function: wakes up every 90 seconds, 
		connects to LiveScores Web Api and pull all game results.
		Normalize the results into LiveScoresResult class, and push the results into 
		Azure Message Queue named: "gamesfeed".

Services:
	DBService: Abstraction layer for Azure CosmosDB Services
	QueueService: Abstraction layer for Azure Queuing services
	SearchIndexService: Abstraction layer for Azure Search indexing services.

Common:
	Contains solution common structures.

RepositorySync:
	Stateless Azure Function that recieves event for every message queued into "gamesfeed"
	queue, check for DB duplication, and insert it to DB if not already exists.

ScoresAPIService:
	API Controller, recieves request with date range parameters, 
	query the DB, using Search index service, and returnes result.

	
	