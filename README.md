# 200OKer
A command-line tool to help with web application warmup and basic OKness checking

## Usage
Build the project. The build results will include `appsettings.json` file and any `*.txt` file you have in the root folder. To run:

`dotnet <path_to_dll>/200oker.dll <path_to_checks_file>/checks.txt`

## Checks file format
Any line that starts with `#` is a comment and will be ignored. Empty lines will be ignored too.
Line format:

`<url_to_start_with>|<xpath_selector_for_links_to_check>`

For example:

`http://www.carsales.com.au/|//*[@class='nav-top-level site']//a`