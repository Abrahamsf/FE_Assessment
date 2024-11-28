# Selenium webdriver in C# with Page Object Model design pattern

This framework is implemented for web-based automation projects and developed using a selenium tool and page object model design pattern. It can be used as a template for selenium projects and flexibility to change the code per the project requirements.
It supports chrome, firefox, and IE browser for local environment testing with an option to further extend to remote execution (Eg. BrowserStack). Also, it supports parallel execution with proper report generation with the use of Extent Reports.
* Note: It can run on the Cloud (Remote) also like the Browser stack etc. Browser stack code is integrated.

# Pre-requisites
* Windows OS / Mac OS - I have made this possible to execute on both as CICD platforms tend to differ
* Visual Studio

# Get Started
Git clone URL - https://github.com/Abrahamsf/FE_Assessment.git

# Installation

1. Visual Studio
2. Start Visual Studio
3.  Click on File>>Open>> Project / Solution Navigate to the project folder and select "FE_Assessment.sln"
                    Or 
Navigate to the project folder and click on "FE_Assessment.sln"

# Configuration setup required:
In folder structure, open app.config file, pass the browser, and run environment details.
* Browser: On which Browser do you want to execute selenium scripts - POC was tested in Chrome
* Environment: In which Environment do you want to run your test case. Ex: QA, UAT or PROD - POC is setup for QA
* RunEnvironment: Where you want to run your test suite, Local or Remote (Cloud) - POC is setup for local execution

In folder structure, Navigate to the Resource folder and open the #Environment.json file. Here you need to add a Website URL based on the Environments

# How to write a Testcase
1. Navigate to the TestSuites folder, and open Smoke.cs file
* Add a TestCase Method starting with TestCase_id.Note: add test case method name starting with testcase_id.
* Navigate to the Pages folder, Open the HomePage.cs file, this is where poage objects is stored, different pages would be stored seperately 
* Once pages are added, come back to the smoke class file, and refer to existing test method examples, of how to call the page class methods.

2. In order to pass test data to the test case. You can pass the test data in multiple ways: 
* Add test data in the Constant file and pass the test data in test case method
* Add test data in Resources>> Testdata.xml file as per the test case format.

# How to execute the TestCase 
* Set the "RunEnvironment" value in app.config file. Ex: Local or Remote.
* Under the menu click on Build> Build Solutionb. Once the solution is built successfully. Under the menu click on View >> Test Explorer
Here you can see the list of test cases. Expand the project tree and right click on the test method and run it.

* To Execute code through Terminal, Navigate to Menu View>> Terminal
1. Navigate to project folder. Ex: cd FE_Assessment.csproj
2. Run following command, dotnet test FE_Assessment.csproj 
It will execute testcase in parllel method.


# Report
* An extent report is used to generate the report. Once execution is completed, navigate to the Reports folder, right-click on index.html file and open it with the respective system browser.
* I have done an execution of the tests so the results of that will be stored in index_2.html as index.html will be overwritten with the next execution.
* I have added screenshots to each action within the report for better clarity.

# NUnit
* NUnit is being utilised to as a testing framework, it makes identifying and executing test cases simpler.
