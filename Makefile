build:
	dotnet build

publish:
	dotnet build
	dotnet build -t:Publish -p:ModPublisherCommand=NewVersion

update-metadata:
	dotnet build -t:Publish -p:ModPublisherCommand=Update
