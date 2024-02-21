truncate table user_follows_users_jt cascade;
truncate table users cascade;
truncate table yeet_has_tags cascade;
truncate table yeets cascade;
truncate table tags cascade;
truncate table yomments cascade;

insert into users (user_id, username, first_name, last_name, password_hash) 
values (1, 'clemens373', 'Clemens', 'Schlipfinger', '$2a$12$LRDbhXnPTk5yQOd6aU8vUegFtKraLOyygBLr552UNdPPx90e9MGZi'),
       (2, 'trueberryless', 'Felix', 'Schneider', '$2a$10$gqhcCliMddK5Q7hXPLXsr./sTXMkcEEc6yelta6GcVdLKdKBxPNcq'),
       (3, 'yanik007', 'Yanik', 'Latzka', '$2a$10$gqhcCliMddK5Q7hXPLXsr./sTXMkcEEc6yelta6GcVdLKdKBxPNcq');

insert into user_follows_users_jt (follower_id, following_id)
values (1, 2),
       (3, 2),
       (3, 1);

insert into tags (tag_id, name)
values (1, 'ice cream'),
       (2, 'mountains'),
       (3, 'politics'),
       (4, 'domestic'),
       (5, 'wizardry');

insert into yeets (yeet_id, title, body, likes, created_at, user_id)
values (1, 'Exciting Journey', 'Embarking on a new adventure. #mountains #nature', 0, current_timestamp, 2),
       (2, 'Tech Innovations', 'Exploring the latest tech innovations. #technology #innovation', 0, current_timestamp, 3),
       (3, 'Thought-provoking Read', 'Reading a thought-provoking book. #literature #philosophy', 0, current_timestamp, 1),
       (4, 'Random Musings', 'Random thoughts and musings. #random #creative', 0, current_timestamp, 2);

insert into yeet_has_tags (yeet_id, tag_id)
values (1, 1),
       (1, 2),
       (2, 3),
       (3, 2),
       (3, 5);

insert into yomments (yomment_id, yeet_id, body, likes, created_at, user_id)
values (1, 1, 'Love the mountain vibes in this yeet!', 0, current_timestamp, 1),
       (2, 1, 'Incredible tech advancements! Can"t wait to see the future.', 0, current_timestamp, 2),
       (3, 2, 'What book are you reading? I"m looking for recommendations.', 0, current_timestamp, 3),
       (4, 3, 'Great thoughts! Creativity knows no bounds.', 0, current_timestamp, 1),
       (5, 3, 'The combination of #mountains and #wizardry is intriguing!', 0, current_timestamp, 2);
