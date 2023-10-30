
Explanation : 
 ![image](https://github.com/shahinibrahimli/Rectangle/assets/58074643/49138f59-1ed0-453b-ab0c-cef6ad92d50e)

 

In architect I used 5 Layer. 
1) Test Layer :
   - XUnit used for implement test
  - SQL Lighter used for mocking DB
  - Auto fixture added for creating mock Data (I did not use that anywhere. Due of less time)
2) Rectangle Api Layer :
  - For APIs I used GraphQL. Mutations.cs Api uses for manipulating data. Query.cs APIs used for Get data.
  - Also added ErrorHandlerMiddleware for handle type of Errors. 
 ![image](https://github.com/shahinibrahimli/Rectangle/assets/58074643/edc271a2-0910-4c36-b564-90d3205652fd)


3) Domain Layer to keep Domain entities.
 - Just for create Foreign key and Index in DB I created this additional table.
 ![image](https://github.com/shahinibrahimli/Rectangle/assets/58074643/ece7000b-e5d5-43ff-b51c-3768c48bf436)


4) Domain.Services Layer for Services for each domain. 
   - This will make testability more efficient
   - Actions will happen there. And maintainability will be easy.
5) Persistence Layer for Entity framework related actions.
 ![image](https://github.com/shahinibrahimli/Rectangle/assets/58074643/666462bd-d728-444b-8011-3fcd3fdb28ab)

 - In the migration folder, there is a migration for adding seed data - 200 rectangles into DB
 - In there I used UnitOfWork Pattern and Factory Pattern with IAuditContextFactory. Each of them can be used to manipulate DB objects.
 -  AuditContext is used to make Entities auditable. In every table additional columns will be like "CreatedDate , LastModifiedDate" and they will set automatically
  

Please please for understanding, take a look at the project and Architect. I really give effort to complete it.
On Wed, Oct 25, 2023 at 7:21 PM Arthur Prishchepov (via Google Docs) <drive-shares-dm-noreply@google.com> wrote:
Arthur Prishchepov shared a document Arthur Prishchepov (prishchepov.arthur@gmail.com) has invited you to comment on the following document: Coding Exercise 114M - SEARCH APIOpenIf you don't want to receive files from this person, block the sender from DriveGoogle LLC, 1600 Amphitheatre Parkway, Mountain View, CA 94043, USA
 You have received this email because prishchepov.arthur@gmail.com shared a document with you from Google Docs. # Rectangle
