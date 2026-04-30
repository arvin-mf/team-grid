CREATE TABLE teams (
    id SERIAL PRIMARY KEY,
    team_number INT NOT NULL,
    class_id INT NOT NULL REFERENCES classes(id) ON DELETE CASCADE
);