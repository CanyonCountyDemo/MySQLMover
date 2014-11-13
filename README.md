MySQLMover
==========

Code to move data from MySQL to SQLServer

As is, this example will not build. You will need to create a new Settings file that has two connection string properties. One titled "MySQL" and the other "SQLServer". It should be obvious as to what those values need to be.

  - MySQL - The connection string to the MySQL server you want to read data from
  - SQLServer - The connection string to the SQL Server you want to move data to.

Yes, I know there's SSIS, but this was easier.