SET @UserGuid = '84a60e81-0f75-4f36-9cf4-018b5fe4ff34';
SET @1Guid = '4572eee1-3619-4df0-8410-185238b110e1';
SET @2Guid = '49d80425-2dfa-478a-b4f6-8298e12fdc80';
SET @3Guid = '7eba4ff0-5fb7-479c-b149-4307bb8495b4';

CREATE TABLE IF NOT EXISTS tasks.Tasks (
    Id char(36) PRIMARY KEY,
	Id_block char(36),
	Id_user char(36) NOT NULL, 
	Title VARCHAR(124) NOT NULL,
	Description TEXT
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_0900_ai_ci;


INSERT IGNORE INTO tasks.Tasks (Id, id_block, id_user, title, description) 
	VALUES (@1Guid,null,@UserGuid,'Tarea TEST 1','TEST 1 descripción tarea test');

INSERT IGNORE INTO tasks.Tasks (Id, id_block, id_user, title, description) 
	VALUES (@2Guid,null,@UserGuid,'Tarea TEST 2','TEST 2 descripción tarea test');
	
INSERT IGNORE INTO tasks.Tasks (Id, id_block, id_user, title, description) 
	VALUES (@3Guid,null,@UserGuid,'Tarea TEST 3','TEST 3 descripción tarea test');