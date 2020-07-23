Solution structure:

Feeds:
	LiveScoreFeed: 
		Stateless Azure Timer Function: wakes up every 90 seconds, 
		connects to LiveScores Web Api and pull all game results.
		Normalize the results into LiveScoresResult class, and push the results into 
		Azure Message Queue named: "gamesfeed".

Services:
	DBService: Abstraction layer for Azure CosmosDB Services
		remark: the database is partitioned, using PartitionKey: Sport
	QueueService: Abstraction layer for Azure Queuing services
	SearchIndexService: Abstraction layer for Azure Search indexing services.

Common:
	Contains solution common structures.

RepositorySync:
	Stateless Azure Function that recieves event for every message queued into "gamesfeed"
	queue, check for DB duplication, and insert it to DB if not already exists.

ScoresAPIService:
	API Controller, hosted in Micrsoft Service Fabric cluster.
	The controller recieves requests with date range parameters, 
	queries the DB, using Search index service, and returne the results.


The client url is:
	http://localhost:8192/api/scores/getscores

	example for querying scores:
	http://localhost:8192/api/scores/getscores?from=2020-07-22T17:59:00&to=2020-07-23T18:01:00