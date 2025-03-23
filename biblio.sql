create database biblio
use biblio 

create table authors (
    id int identity(1,1) primary key,
    name nvarchar(255) not null
);

create table books (
    id int identity(1,1) primary key,
    title nvarchar(255) not null,
    available bit default 1
);

create table book_authors (
    book_id int,
    author_id int,
    foreign key (book_id) references books(id) on delete cascade,
    foreign key (author_id) references authors(id) on delete cascade,
    primary key (book_id, author_id)
);

create table visitors (
    id int identity(1,1) primary key,
    name nvarchar(255) not null,
    is_debtor bit default 0
);

create table loans (
    id int identity(1,1) primary key,
    visitor_id int,
    book_id int,
    loan_date datetime default getdate(),
    foreign key (visitor_id) references visitors(id) on delete cascade,
    foreign key (book_id) references books(id) on delete cascade
);

insert into authors (name) values 
('Taras Shevchenko'),
('Lina Kostenko'),
('Leo Tolstoy'),
('George Orwell'),
('Ray Bradbury');

insert into books (title, available) values 
('Kobzar', 1),
('Notes of a Ukrainian Madman', 1),
('War and Peace', 1),
('1984', 0),
('Fahrenheit 451', 1);

insert into book_authors (book_id, author_id) values 
(1, 1),
(2, 2), 
(3, 3), 
(4, 4),
(5, 5);

insert into visitors (name, is_debtor) values 
('Alexander', 0),
('Marina', 1),
('Ivan', 0),
('Olga', 1),
('Dmitry', 0);

insert into loans (visitor_id, book_id, loan_date) values 
(2, 4, '2024-03-01'),
(4, 3, '2024-02-20'),
(1, 5, '2024-03-10'),
(3, 1, '2024-03-05'),
(5, 2, '2024-03-07'); 