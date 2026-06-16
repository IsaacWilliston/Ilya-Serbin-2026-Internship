-- Cinemas
INSERT INTO base_schema.cinemas (name, address, city) VALUES
  ('CineMega', 'ul. Amir Temur 1', 'Tashkent'),
  ('CinePlex', 'ul. Navoi 5',      'Tashkent');

-- Halls
INSERT INTO base_schema.halls (cinema_id, name) VALUES
  (1, 'Hall A'),
  (1, 'Hall B'),
  (2, 'Hall 1');

-- Movies
INSERT INTO base_schema.movies (title, "duration-minutes", "age-rating", rating, "poster-url", description, "release-year") VALUES
  ('Interstellar',             169, 'PG_13', 8.7, 'https://example.com/interstellar.jpg', 'A team of explorers travel through a wormhole in space.',  2014),
  ('The Dark Knight',          152, 'PG_13', 9.0, 'https://example.com/dark-knight.jpg',  'Batman faces the Joker, a criminal mastermind.',           2008),
  ('Inception',                148, 'PG_13', 8.8, 'https://example.com/inception.jpg',    'A thief enters dreams to plant an idea.',                  2010),
  ('The Grand Budapest Hotel',  99, 'R',     8.1, 'https://example.com/budapest.jpg',     'The adventures of a legendary hotel concierge.',           2014);

-- Movie genres
INSERT INTO base_schema.movie_genres (movie_id, genre) VALUES
  (1, 'ADVENTURE'), (1, 'DRAMA'),  (1, 'SCI_FI'),
  (2, 'ACTION'),    (2, 'DRAMA'),  (2, 'THRILLER'),
  (3, 'ACTION'),    (3, 'SCI_FI'), (3, 'THRILLER'),
  (4, 'ADVENTURE'), (4, 'COMEDY'), (4, 'DRAMA');

-- Price categories
INSERT INTO base_schema.price_category (type, name, price) VALUES
  ('LUXURY',  'VIP Recliner', 25.00),
  ('REGULAR', 'Standard',     12.00),
  ('ECONOMY', 'Economy',       8.00);

-- Seats: Hall A (hall_id=1) — 3 rows x varying seats
INSERT INTO base_schema.seats (hall_id, price_category_id, "row", "number", status, is_available, comment) VALUES
  (1, 1, 1, 1, 'ACTIVE', true,  'VIP front-left'),
  (1, 1, 1, 2, 'ACTIVE', true,  'VIP front-right'),
  (1, 2, 2, 1, 'ACTIVE', true,  NULL),
  (1, 2, 2, 2, 'ACTIVE', true,  NULL),
  (1, 2, 2, 3, 'ACTIVE', true,  NULL),
  (1, 2, 3, 1, 'ACTIVE', true,  NULL),
  (1, 2, 3, 2, 'ACTIVE', true,  NULL),
  (1, 3, 3, 3, 'ACTIVE', true,  NULL),
  (1, 3, 3, 4, 'ACTIVE', true,  NULL),
  (1, 3, 3, 5, 'ACTIVE', true,  NULL);

-- Seats: Hall B (hall_id=2) — 2 rows x 4 seats
INSERT INTO base_schema.seats (hall_id, price_category_id, "row", "number", status, is_available, comment) VALUES
  (2, 1, 1, 1, 'ACTIVE', true,  'VIP'),
  (2, 1, 1, 2, 'ACTIVE', true,  'VIP'),
  (2, 2, 2, 1, 'ACTIVE', true,  NULL),
  (2, 2, 2, 2, 'ACTIVE', true,  NULL),
  (2, 2, 2, 3, 'ACTIVE', false, 'Broken armrest'),
  (2, 3, 2, 4, 'ACTIVE', true,  NULL);

-- Sessions
INSERT INTO base_schema.sessions (movie_id, hall_id, title, "date", "time", language, format) VALUES
  (1, 1, 'Interstellar - Morning',   CURRENT_DATE,     '10:00', 'ENGLISH', 'IMAX'),
  (2, 1, 'Dark Knight - Evening',    CURRENT_DATE,     '19:30', 'ENGLISH', 'TWO_D'),
  (3, 2, 'Inception - Afternoon',    CURRENT_DATE,     '14:00', 'RUSSIAN', 'THREE_D'),
  (1, 2, 'Interstellar - Tomorrow',  CURRENT_DATE + 1, '11:00', 'ENGLISH', 'IMAX'),
  (4, 3, 'Grand Budapest - Evening', CURRENT_DATE + 1, '20:00', 'ENGLISH', 'TWO_D');

-- Session seats (some booked, most available)
INSERT INTO base_schema.session_seats (session_id, seat_id, status, is_available, customer_name, contact) VALUES
  -- Session 1 (Interstellar morning, Hall A)
  (1, 1,  'ACTIVE', 'false', 'Alice Johnson', '+998901234567'),
  (1, 2,  'ACTIVE', 'false', 'Bob Smith',     '+998907654321'),
  (1, 3,  'ACTIVE', 'true',  NULL, NULL),
  (1, 4,  'ACTIVE', 'true',  NULL, NULL),
  (1, 5,  'ACTIVE', 'false', 'Carol White',   '+998909988776'),
  (1, 6,  'ACTIVE', 'true',  NULL, NULL),
  (1, 7,  'ACTIVE', 'true',  NULL, NULL),
  (1, 8,  'ACTIVE', 'true',  NULL, NULL),
  (1, 9,  'ACTIVE', 'true',  NULL, NULL),
  (1, 10, 'ACTIVE', 'true',  NULL, NULL),
  -- Session 2 (Dark Knight evening, Hall A)
  (2, 1,  'ACTIVE', 'true',  NULL, NULL),
  (2, 2,  'ACTIVE', 'true',  NULL, NULL),
  (2, 3,  'ACTIVE', 'false', 'David Lee',     '+998911223344'),
  (2, 4,  'ACTIVE', 'false', 'Eva Martinez',  '+998912345678'),
  (2, 5,  'ACTIVE', 'true',  NULL, NULL),
  (2, 6,  'ACTIVE', 'false', 'Frank Brown',   '+998913456789'),
  (2, 7,  'ACTIVE', 'true',  NULL, NULL),
  (2, 8,  'ACTIVE', 'true',  NULL, NULL),
  (2, 9,  'ACTIVE', 'true',  NULL, NULL),
  (2, 10, 'ACTIVE', 'true',  NULL, NULL),
  -- Session 3 (Inception afternoon, Hall B)
  (3, 11, 'ACTIVE', 'false', 'Grace Kim',    '+998914567890'),
  (3, 12, 'ACTIVE', 'false', 'Henry Wilson', '+998915678901'),
  (3, 13, 'ACTIVE', 'true',  NULL, NULL),
  (3, 14, 'ACTIVE', 'true',  NULL, NULL),
  (3, 15, 'DEACTIVATED', 'false', NULL, NULL),
  (3, 16, 'ACTIVE', 'true',  NULL, NULL);
