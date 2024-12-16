export interface Response {
    id: Guid;
    playerId: Guid; // ID игрока
    eventId: Guid; // ID события
    responseOption: number; // Вариант ответа (1, 2 или 3)
}
