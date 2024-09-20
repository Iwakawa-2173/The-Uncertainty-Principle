-- Создание таблицы игроков
CREATE TABLE players (
    player_id SERIAL PRIMARY KEY,
    unique_id VARCHAR(255) NOT NULL UNIQUE,  -- Уникальный ID игрока
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Создание таблицы событий
CREATE TABLE events (
    event_id SERIAL PRIMARY KEY,
    event_number INT NOT NULL,  -- Номер события от 1 до 100
    is_significant BOOLEAN NOT NULL DEFAULT FALSE,  -- Сюжетно значимое событие
    description TEXT NOT NULL  -- Описание события
);

-- Создание таблицы ответов
CREATE TABLE responses (
    response_id SERIAL PRIMARY KEY,
    player_id INT REFERENCES players(player_id) ON DELETE CASCADE,  -- Связь с игроком
    event_id INT REFERENCES events(event_id) ON DELETE CASCADE,  -- Связь с событием
    response_option INT NOT NULL CHECK (response_option IN (1, 2, 3)),  -- Вариант ответа (1, 2 или 3)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Дата и время ответа
);

-- Создание таблицы шкал очков
CREATE TABLE score_scales (
    scale_id SERIAL PRIMARY KEY,
    player_id INT REFERENCES players(player_id) ON DELETE CASCADE,  -- Связь с игроком
    scale_1 INT DEFAULT 0,  -- Очки за вариант 1
    scale_2 INT DEFAULT 0,  -- Очки за вариант 2
    scale_3 INT DEFAULT 0,  -- Очки за вариант 3
    last_event_id INT REFERENCES events(event_id) ON DELETE SET NULL,  -- Последнее событие
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Дата и время обновления очков
);

-- Создание таблицы чатов
CREATE TABLE chat_messages (
    message_id SERIAL PRIMARY KEY,
    player_id INT REFERENCES players(player_id) ON DELETE CASCADE,  -- Автор сообщения
    message TEXT NOT NULL,  -- Текст сообщения
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Дата и время сообщения
);