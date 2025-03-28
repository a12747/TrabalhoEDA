#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include "lista.h"

#define MAX_LINHA 256

Antena* lista = NULL;
Localizacao* efeitos = NULL;

void AdicionarAntena(char freq, int x, int y) {
    Antena* nova = (Antena*)malloc(sizeof(Antena));
    nova->frequencia = freq;
    nova->x = x;
    nova->y = y;
    nova->next = lista;
    lista = nova;
}

void RemoverAntena(int x, int y) {
    Antena **indirect = &lista;
    while (*indirect) {
        if ((*indirect)->x == x && (*indirect)->y == y) {
            Antena* temp = *indirect;
            *indirect = temp->next;
            free(temp);
            printf("Antena removida!\n");
            return;
        }
        indirect = &(*indirect)->next;
    }
    printf("Nenhuma antena para remover!\n");
}

void MostrarAntenas() {
    Antena* atual = lista;
    if (!atual) {
        printf("Nenhuma antena adicionada!\n");
        return;
    }
    while (atual) {
        printf("Frequência: %c, Posição: (%d, %d)\n", atual->frequencia, atual->x, atual->y);
        atual = atual->next;
    }
}

static int DentroDosLimites(int x, int y, int largura, int altura) {
    return x >= 0 && y >= 0 && x < largura && y < altura;
}

static void AdicionarEfeito(int x, int y) {
    Localizacao* nova = (Localizacao*)malloc(sizeof(Localizacao));
    nova->x = x;
    nova->y = y;
    nova->next = efeitos;
    efeitos = nova;
}

static void LimparEfeitos() {
    while (efeitos) {
        Localizacao* temp = efeitos;
        efeitos = efeitos->next;
        free(temp);
    }
}

void ValidarEfeitos() {
    LimparEfeitos();
    for (Antena* a1 = lista; a1; a1 = a1->next) {
        for (Antena* a2 = a1->next; a2; a2 = a2->next) {
            if (a1->frequencia != a2->frequencia) continue;
            int dx = a2->x - a1->x;
            int dy = a2->y - a1->y;
            if (dx % 2 != 0 || dy % 2 != 0) continue;
            int largura = (a1->x > a2->x ? a1->x : a2->x) + 10;
            int altura = (a1->y > a2->y ? a1->y : a2->y) + 10;
            int px1 = a1->x - dx;
            int py1 = a1->y - dy;
            int px2 = a2->x + dx;
            int py2 = a2->y + dy;
            if (DentroDosLimites(px1, py1, largura, altura)) AdicionarEfeito(px1, py1);
            if (DentroDosLimites(px2, py2, largura, altura)) AdicionarEfeito(px2, py2);
        }
    }
}

void MostrarEfeitos() {
    if (!efeitos) {
        printf("Nenhuma antena adicionada!\n");
        return;
    }
    printf("Localizações com efeito nefasto:\n");
    for (Localizacao* atual = efeitos; atual; atual = atual->next) {
        printf("Coordenadas: (%d, %d)\n", atual->x, atual->y);
    }
}

void CarregarFicheiro(const char* caminho) {
    FILE* file = fopen(caminho, "r");
    if (!file) {
        printf("Ficheiro não encontrado.\n");
        return;
    }
    char linha[MAX_LINHA];
    int y = 0;
    while (fgets(linha, sizeof(linha), file)) {
        for (int x = 0; linha[x] != '\0'; x++) {
            if (isalnum(linha[x])) {
                AdicionarAntena(linha[x], x, y);
            }
        }
        y++;
    }
    fclose(file);
    ValidarEfeitos();
}
