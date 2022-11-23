# MeDirect
Run the required files to run in MSSQL.

Within the project; You will see it under the "Doc" folder.

The study was carried out in accordance with the monolothotic working principle.
https://dotnetkonf.az/resources/Architecting-Cloud-Native-NET-Apps-for-Azure.pdf

- Cache the entire application with InMemoryDatabase, use it. (optional)

- SeriLog kullandım. (log için)

- I used QUARTZ. (I matched the foreign currency type of the master account with the foreign currency defined in the system and stored it in the database. (If there is no storage process, it stores it.)

- I added the control process in the last 1 hour.

- When an exchange rate is used it should never be older than 30 minutes

  (I storage to data in DB and also im checking with QUARTZ)



You can focus to this path.

...\MeDirect.WebApi\Controllers\v1\BankAccountActivityController.cs

The CREATE POST api/controller here should be examined.



Database :

1. MEDIRECTDB.bak 
2. MEDIRECTIDENTITYDB.bak (For Identity)



API Auth. Information (For Token)

Username : info@medirect.com
Password : 123Pa$$word!



In general, the steps in the project;

Bank - (Banks table)
- HSBC (example)

Bank Account
- HSBC USD ACCOUNT (example)

Bank Account Type (Bank account type table)
- CASH Account or CREDIT Account

Bank Account Activity (Bank account activities)
- Bank Account ID => Bank Account
- Quantity => Number to convert
- Currency Code => Currency you want to convert
- Balance => Result value of this person after conversion
- Process ID => BUY or SELL

A little scenario:

>> I have USD account. (BankAccount)
>> How is the purchase process?

When trading with this account,

>> How about getting 100 EURO?

Ex. : 100 EURO => 103 dollars would be to buy. 103 USD is added to my account.


>> I have USD account. (BankAccount)
>> How is the sale process?

Ex. : When I want to sell 100 EURO => You will get an error message saying you cannot sell on USD account.