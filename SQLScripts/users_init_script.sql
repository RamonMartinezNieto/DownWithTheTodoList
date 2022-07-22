SET @Nichname = 'admin';
SET @Pass = 'passwordhasheada';
SET @Guid = '84a60e81-0f75-4f36-9cf4-018b5fe4ff34'; 

CREATE TABLE IF NOT EXISTS users.Users (
    Id char(36) PRIMARY KEY,
	NickName VARCHAR(128) NOT NULL UNIQUE,
	Pass VARCHAR(1024) NOT NULL
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_0900_ai_ci;

INSERT IGNORE INTO users.Users (Id, NickName, Pass) 
VALUES (@Guid,@Nichname,@Pass)
