app:
	dotnet run --project TeamGrid

migrate:
	dotnet run --project TeamGrid.Migrator

compose-up:
	docker compose up -d

compose-down:
	docker compose down

compose-logs-db:
	docker compose logs -f db

test:
	dotnet test TeamGrid.Tests/TeamGrid.Tests.csproj
