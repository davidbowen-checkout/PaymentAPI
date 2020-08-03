This project consists of three main elements:
1) A Fake Bank. This uses a dictionary to store values / account details.
2) A Payment Gateway api. This contains access to both create new payment requests and get previous payment requests from a unique ID.
3) A Payment Gateway Agent. This processes payments that are access from the Gateway via the "Tasks" controller. This controller would be seperated out for further use, but is left inplace for now for simplicity.

I originally intended to use RabbitMq as a more robust method of ensuring that payments are processed as a seperate function to being requested. This was important as we're not in control of any (theoretical) delays that may be caused by the banking API.
Unfortunately, I couldn't guarantee that dependencies would be available on the host machine, so I moved away from this method.
In addition to this I addressed Idempotency by creating a unique hash to identify every transaction. The hashing mechanism can be swapped out as neccessary. This might be important if you're looking to change the definition of a "duplicate" transaction. 
As an example of this, timestamp (minus seconds) is currently present within the hashing mechanism. This means after 1minute, the user could re-submit the same request and make a second payment, this was intentional but can be adjusted by changing the hashing algorithm to not include time.


Assumptions:
1) The gateway is for a single company, like Amazon. Therefore the payments will be paid into a set account.
2) Transactions submittted twice in the same minute are erroneous.
3) The tasks controller would be seperated out and secured seperately. This would prevent anyone from having full access to payment data.
4) The API and agent would be run together, but Payments can be requested without functioning agents.