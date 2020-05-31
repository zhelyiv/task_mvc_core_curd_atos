use AtsTest
go

create table Tags
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(512),
)
go
 
create table [User]
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	[Login] varchar(256)
)
go
 
create table Blogs
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	OwnerUserId int not null,

	FOREIGN KEY (OwnerUserId) REFERENCES [User](Id)
)
go
 
create table UserBlogs
( 
	Id int IDENTITY(1,1) PRIMARY KEY,
	UserId int not null,
	BlogId int not null,
	

	FOREIGN KEY (UserId) REFERENCES [User](Id),
	FOREIGN KEY (BlogId) REFERENCES Blogs(Id),

	CONSTRAINT [UQ_UserBlog] UNIQUE NONCLUSTERED
    (
        UserId, BlogId
    )
)
go

create table Posts
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Contents varchar(max), 
	BlogId int not null,

	FOREIGN KEY (BlogId) REFERENCES Blogs(Id)
)
go

create Table PostTags
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	PostId int not null,
	TagId int not null,

	FOREIGN KEY (PostId) REFERENCES Posts(Id),
	FOREIGN KEY (TagId) REFERENCES Tags(Id)
)
go

create table Comments
(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Contents varchar(max), 
	UserId int not null,
	PostId int not null,
	 
	FOREIGN KEY (UserId) REFERENCES [User](Id),
	FOREIGN KEY (PostId) REFERENCES Posts(Id)
)
go
 
  