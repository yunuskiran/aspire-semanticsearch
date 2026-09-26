# Aspire Semantic Search

Semantic search over a small event catalogue, built to understand where the moving parts of a
retrieval feature actually live: who owns the embedding call, where vectors are stored, and what
the API surface looks like when there is no vector database in the picture.

.NET 9 · .NET Aspire · Microsoft Semantic Kernel · in-memory vector store · no external services.

## Why it looks like this

Most retrieval samples start with a hosted vector database, which hides the two decisions that
matter: **when** you embed and **what** you compare. Keeping the store in memory makes both
explicit — embeddings are generated on write, similarity is cosine over float arrays, and the
whole path is visible in `EventAiService`.

Aspire is here because the same feature in production is never one process: an API, a model
provider and a store, wired with service discovery and health checks. Aspire makes that shape
the default instead of an afterthought.

## Shape

```
SemanticSearch.AppHost/        Aspire orchestration
Api/
  Models/Event.cs              domain record
  Models/EventVector.cs        embedding + payload
  Services/EventAiService.cs   embedding generation and cosine search
  Data/EventDbContext.cs       EF Core persistence
  Api.http                     request collection (used instead of Swagger)
SemanticSearch.ServiceDefaults/ telemetry, health checks, resilience
```

## Running it

```bash
dotnet run --project SemanticSearch.AppHost
```

Then use `Api/Api.http` to add events and query them. Two endpoints: one writes an event and
stores its embedding, one takes a natural-language query and returns ranked matches.

```
POST /events          { "title": "...", "description": "..." }
GET  /events/search   ?q=live jazz near the river
```

## What I took away from it

- **Chunking is the whole game.** With short event descriptions, one embedding per record is
  fine. The moment records get long, top-k over whole documents starts returning the right
  document and the wrong passage — that is where chunk size and overlap stop being cosmetic.
- **Cosine similarity has no absolute meaning.** A score of 0.78 is only interpretable against
  the distribution of your own corpus, so a fixed threshold is a bug waiting to happen. Rank,
  then cut by relative gap.
- **The embedding call belongs behind an interface.** Swapping model or dimension count should
  not touch the search code. It did at first, which is why `EventAiService` exists.
- **In-memory is the right default for learning and the wrong one for production.** Restart
  loses the index and rebuild cost scales linearly with the corpus; the next step is a real
  store (MongoDB Atlas Vector Search or pgvector) behind the same interface.

## Not in scope

No reranking, no hybrid keyword+vector search, no persistence of vectors across restarts, no
evaluation harness. For the evaluation side of model-backed features, see
[llm-subscription-terms-extraction](https://github.com/yunuskiran/llm-subscription-terms-extraction).
