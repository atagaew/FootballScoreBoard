# Live Football World Cup Scoreboard Library

## Overview

Welcome to the Live Football World Cup Scoreboard library! This library is designed to help you manage and display live scores for ongoing football matches during the World Cup. Whether you are a sports data company, a developer building a football-related application, or a fan looking for real-time updates, this library has got you covered.

## Features

1. **Start a New Match**
   - Initialize a new match with the starting score set to 0 – 0.
   - Capture essential parameters such as home team and away team.

2. **Update Score**
   - Dynamically update the score of a match by providing absolute scores for both the home team and away team.

3. **Finish Match**
   - Remove a match that is currently in progress from the scoreboard.

4. **Get Summary of Matches in Progress**
   - Retrieve a summary of ongoing matches.
   - Matches are ordered by their total score.
   - In case of matches with the same total score, the order is determined by the most recently started match in the scoreboard.

## Getting Started

To integrate the Live Football World Cup Scoreboard library into your project, follow these simple steps:

1. Download the library files or add them as a project reference.

2. Register the library services in your application startup class.

```C#
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddFootballScoreBoardServices();
        });
```

3. Inject the library services into your application classes.

```C#
    private readonly IMatchService _matchService;
    private readonly IMatchReportService _matchReportService;

    public ConsoleApplication(IMatchService matchService, IMatchReportService matchReportService)
    {
        _matchService = matchService;
        _matchReportService = matchReportService;
    }
```	

4. Start managing live scores and matches!

```C#
    // Initialize a new match
    var mexicoCanada = _matchService.Create(new Match("Mexico", "Canada")
    _matchService.Create(mexicoCanada);

    // Update the score of an ongoing match
    _matchService.Update(mexicoCanada.UpdateScore(0, 6));

    // Finish a match
    _matchService.Finish(mexicoCanada.FinishMatch(0, 6));

    // Get a summary of ongoing matches
    _matchReportService.Summary();
```

You can check working application in the [SportRadar.FootballScoreBoard.Playground](https://github.com/atagaew/FootballScoreBoard/blob/master/src/SportRadar.FootballScoreBoard.Playground/ConsoleApplication.cs) project.

## Contribution Guidelines

We welcome contributions from the community to enhance the functionality and usability of the Live Football World Cup Scoreboard library. If you have ideas for improvement, bug fixes, or new features, please open an issue or submit a pull request.

## License

This library is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Feel free to use and modify the library according to your needs. Happy coding and enjoy the football season! ⚽🏆