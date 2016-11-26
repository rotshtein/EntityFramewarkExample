1.	From the marketplace using the NiGet manager
	a.	Log4net (2.0.5)
	b.	EntityFramework (6.1.3)
2.	Install SQLEXPRESS server
	a.	Create database called RIT_QA
	b.	From the Model.edmx “Generate Database from Model” and save to Server
3. In the Log4Net.config file set the location of the logfile
4. Following http://nowfromhome.com/msbuild-add-git-commit-hash-to-assemblyinfo/ install and config 
   the project file to contain the git version information
