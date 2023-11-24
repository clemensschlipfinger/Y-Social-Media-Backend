INSERT INTO USERS (USERNAME, FIRST_NAME, LAST_NAME, PASSWORD_HASH)
VALUES
    ('user1', 'John', 'Doe', 'hashed_password1'),
    ('user2', 'Jane', 'Smith', 'hashed_password2'),
    ('user3', 'Bob', 'Johnson', 'hashed_password3');

INSERT INTO YEETS (BODY, UserId)
VALUES
    ('This is my first yeet!', 1),
    ('Just yeeted again, feeling good!', 2),
    ('Yeet of the day!', 3);

INSERT INTO USER_LIKES_YEETS_JT (USER_ID, YEET_ID)
VALUES
    (1, 1),  -- User 1 likes Yeet 1
    (1, 2),  -- User 1 likes Yeet 2
    (2, 2),  -- User 2 likes Yeet 2
    (3, 3);  -- User 3 likes Yeet 3

INSERT INTO USER_FOLLOWS_USERS_JT (SLAVE, MASTER)
VALUES
    (1, 2),  -- User 1 follows User 2
    (2, 3),  -- User 2 follows User 3
    (3, 1);  -- User 3 follows User 1



