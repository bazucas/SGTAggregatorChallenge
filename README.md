# Santander Global Tech Aggregator Challenge

Senior backend developer code challenge.
  
<br>

## Demo
Coming soon...

<br>

## Requirements

### User wants to call a webservice and get all Hacker News items aggregated.
This test contains a challenge and it is up to you the amount of time and effort to put on it. We expect
you to spend at least 2-3 hours to complete the challenge.<br>
To submit your results, you should create a public repository with the source code and share the link
with us.<br>
The repository should include a README with instructions on how to run the solution(s) (if applicable)
and any assumptions, comments and information that you find relevant.<br>
In case you didn’t have time to do some enhancements on your code, please describe it on the README.

<br>

### PROBLEM:
The goal of this exercise is to build an item aggregator API of the Hacker News items. The system
should take a list of item IDs from user input and return a count of valid items broken down per type and
per author.<br>
For example, given 5 item IDs:
* 1 of them is a story and the author is Bob
* 3 of them are comments, being 2 from Alice and 1 from Bob
* 1 of them is a poll and the author is Greg
* 1 of them is an invalid item

The system should return the aggregation as follows:

```json
{
  "stories": {
    "Bob": 1
  },
  "comments": {
    "Alice": 2,
    "Bob": 1
  },
  "polls": {
    "Greg": 1
  }
}
```

<br>

### API SPECIFICATION:
The aggregator API should be an endpoint that accepts a HTTP POST request on the path /aggregate

The endpoint should accept a JSON payload on the following schema, where the ids array contains all
IDs to be considered on the aggregation.


```json
{
  "$schema": "http://json-schema.org/draft-07/schema",
  "required": [
    "ids"
  ],
  "type": "object",
  "properties": {
    "ids": {
      "type": "array",
      "items": {
        "type": "string"
      }
    }
  }
}
```

Example:

```json
{
  "ids": ["123", "456", "9999"]
}
```

The endpoint should return a JSON response on the following schema, which represents the result of
the aggregation:

```json
{
  "type": "object",
  "additionalProperties": {
    "type": "object",
    "additionalProperties": {
      "type": "number"
    }
  }
}
```

Example:

```json
{
  "stories": {
    "Bob": 1
  },
  "comments": {
    "Alice": 2,
    "Bob": 1
  },
  "polls": {
    "Greg": 1
  }
}
```

<br>

### HACKER NEWS API:

The Hacker News API documentation is here: https://github.com/HackerNews/API
For this challenge, you will need to use the following Items endpoint, which will give all the information needed for the aggregation: https://github.com/HackerNews/API#items
For the purposes of this challenge, an invalid item is the one that returns null on the GET request to /v0/items/{id}.json. For example:
https://hacker-news.firebaseio.com/v0/item/invalid.json

<br>

### REVIEW:

After the submission, your solution will be evaluated using the same parameters we do on our team code reviews:
* Correctness of the output;
* Usage of the appropriate data structures;
* Best practices adherence;
* Error handling;
* Concurrency handling;
* Clean/Clear code.

<br>

## Running application

Git clone the repository and in the project root folder do a

```bash
dotnet restore
dotnet build
``` 

Using Docker to run the containerized application, go to the Api folder and run
$:docker build -f [DockerFilePath] -t santander --target base [SolutionPath]

```bash
docker build -f ".\Dockerfile" -t santander --target base "..\..\" 
docker run -it -d -p 5000:5000 santander
```

If by some reason Docker won't run or do a build error, run it on VS2019

In alternative, you can run it outside docker by going to the Api folder and run

```bash
dotnet run
```

Use Postman to test the endpoint - https://localhost:5000/api/aggregate

<br>

Pass this in the body:

```json
{
  "ids": ["123", "456","123", "456", "9999"]
}
```

<br>

## Assumptions

* ASPNET 5.0 framework used;
* Requirements and time constraints in mind;
* Simple and clean application with KISS, DRY and YAGNI principles used;
* SOLID Principles in mind, although, didn't overkilled it by creating a single class for everything, DI and interfaces.
* The structure was a onionish/cleanish architecture;
* Test project added, but need some actual unit tests;
* Dictionary of dicyionaries seemed the best option to accomodate dynamic objects;
* Some errors and bad requests treated, although it could have more error handling;
* No database (persistence), ILogger, etc, used;
* Thorough testing is needed;
* I had some issues with Docker and the VMs, couldn't properly test it. 

<br>

## License
[MIT](https://choosealicense.com/licenses/mit/)
