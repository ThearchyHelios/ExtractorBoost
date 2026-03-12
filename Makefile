build:
	dotnet build

publish:
	dotnet build -t:Publish -p:ModPublisherCommand=Update
