INSERT INTO users (username, first_name, last_name, password_hash)
VALUES
    ('user1', 'John', 'Doe', '$2a$10$Z2cEZ5JiyMbkEWsdHLTn2e1CFWDWS0tMIsFX7Od0/o.IX2M7LXa6O'), --password: password
    ('user2', 'Jane', 'Smith', '$2a$10$Z2cEZ5JiyMbkEWsdHLTn2e1CFWDWS0tMIsFX7Od0/o.IX2M7LXa6O'),
    ('user3', 'Bob', 'Johnson', '$2a$10$Z2cEZ5JiyMbkEWsdHLTn2e1CFWDWS0tMIsFX7Od0/o.IX2M7LXa6O');

INSERT INTO yeets (body, user_id, created_at)
VALUES
    ('This is my first yeet!', 1, '2023-12-01'),
    ('Just yeeted again, feeling good!', 2, '2023-11-15'),
    ('Yeet of the day!', 3, '2023-02-01');

INSERT INTO user_likes_yeets_jt (user_id, yeet_id)
VALUES
    (1, 1),  -- User 1 likes Yeet 1
    (1, 2),  -- User 1 likes Yeet 2
    (2, 2),  -- User 2 likes Yeet 2
    (3, 3);  -- User 3 likes Yeet 3

INSERT INTO user_follows_users_jt (slave_id, master_id)
VALUES
    (1, 2),  -- User 1 follows User 2
    (2, 3),  -- User 2 follows User 3
    (3, 1);  -- User 3 follows User 1
