#ifndef LISTA_H
#define LISTA_H

typedef struct Antena {
    char frequencia;
    int x;
    int y;
    struct Antena* next;
} Antena;

typedef struct Localizacao {
    int x;
    int y;
    struct Localizacao* next;
} Localizacao;

extern Antena* lista;
extern Localizacao* efeitos;

void AdicionarAntena(char freq, int x, int y);
void RemoverAntena(int x, int y);
void MostrarAntenas();
void ValidarEfeitos();
void MostrarEfeitos();
void CarregarFicheiro(const char* caminho);

#endif // LISTA_H
