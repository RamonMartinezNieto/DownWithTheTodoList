SET @UserGuid = '84a60e81-0f75-4f36-9cf4-018b5fe4ff34';
SET @1Guid = '1572eee1-3619-4df0-8410-185238b110e1';
SET @2Guid = '29d80425-2dfa-478a-b4f6-8298e12fdc80';
SET @3Guid = '3eba4ff0-5fb7-479c-b149-4307bb8495b4';

CREATE TABLE IF NOT EXISTS blocks.Blocks(
    Id char(36) PRIMARY KEY,
	Id_user char(36) NOT NULL, 
	Title VARCHAR(124) NOT NULL,
	Description TEXT
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_0900_ai_ci;

INSERT IGNORE INTO blocks.Blocks (Id, id_user, title, description) 
	VALUES (@1Guid,@UserGuid,'Block TEST 1','TEST 1 descripción block test');
	
INSERT IGNORE INTO blocks.Blocks (Id, id_user, title, description) 
	VALUES (@2Guid,@UserGuid,'Block TEST 2','TEST 2 descripción block test');
	
INSERT IGNORE INTO blocks.Blocks (Id, id_user, title, description) 
	VALUES (@3Guid,@UserGuid,'Block TEST 3','TEST 3 descripción block test');