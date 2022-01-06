# Padrão CNAB400 - Banco Eximia

## Nomenclatura dos arquivos

| Arquivo      | Conteúdo           |
|--------------|--------------------|
| CCCCCMDD.CRM | Pedidos de boletos |

Onde **CCCCCMDD**:
- **CCCCC** = Código do beneficiário
- **MDD** = Código do mês e número do dia da data de geração do arquivo
- **CRM** = Indica que é o primeiro arquivo da remessa

### Codificação dos meses

| Mês       | Código | Mês      | Código    |
|-----------|--------|----------|-----------|
| Janeiro   | 1      | Julho    | 7         |
| Fevereiro | 2      | Agosto   | 8         |
| Março     | 3      | Setembro | 9         |
| Abril     | 4      | Outubro  | O (letra) |
| Maio      | 5      | Novembro | N         |
| Junho     | 6      | Dezembro | D         |

## Sequência de restros no arquivo

- **HEADER** - único
- **DETALHE** - quantidade variável
- **TRAILER** - único

## Registro **HEADER**

| Posição   | Tamanho | Descrição                           | Conteúdo                             |
|-----------|---------|-------------------------------------|--------------------------------------|
| 001 a 001 | 001     | Identificação do registro *header*  | valor 0 (zero)                       |
| 002 a 002 | 001     | Identificação do arquivo de remessa | valor deve ser 1                     |
| 003 a 009 | 007     | Literal remessa                     | "REMESSA"                            |
| 010 a 011 | 002     | Código do serviço de cobrança       | fixo "01"                            |
| 012 a 026 | 015     | Literal cobrança                    | "COBRANCA"                           |
| 027 a 031 | 005     | Código do beneficário               |                                      |
| 032 a 045 | 014     | CNPF do beneficário                 | Alinhado com zeros a esquerda        |
| 046 a 076 | 031     | Filler                              | Deixar em branco                     |
| 077 a 079 | 003     | Número do banco                     | 987                                  |
| 080 a 094 | 015     | BANCO                               | Literal "EXIMIA"                     |
| 095 a 102 | 008     | Filer                               | Deixar em branco                     |
| 111 a 117 | 007     | Número da remessa                   | Numero do ultimo arquivo de remessa  |
| 118 a 390 | 273     | Filer                               | Deixar em branco                     |
| 391 a 394 | 004     | Versão do sistema                   | "2.00"                               |
| 395 a 400 | 006     | Número sequêncial do arquivo        | Alinhado a direta e zeros à esquerda |

## Registro *Detalhe*

| Posição   | Tamanho | Descrição                                | Conteúdo                                                                        |
|-----------|---------|------------------------------------------|---------------------------------------------------------------------------------|
| 001 a 001 | 001     | Identificação do registro detalhe        | "1"                                                                             |
| 002 a 002 | 001     | Tipo de cobrança                         | "A" - com registro                                                              |
| 003 a 003 | 001     | Tipo de carteira                         | "A" - simples                                                                   |
| 004 a 004 | 001     | Tipo de impressão                        | "A" - normal                                                                    |
| 005 a 016 | 012     | Filer                                    | Deixar em branco                                                                |
| 017 a 017 | 001     | Tipo de moeda                            | "A" - Real                                                                      |
| 018 a 018 | 001     | Tipo de Desconto                         | "A"- Valor ;  "B"- Percentual                                                   |
| 019 a 019 | 001     | Tipo de Juros                            | "A"- Valor ;  "B"- Percentual                                                   |
| 020 a 047 | 028     | Filer                                    | Deixar em branco                                                                |
| 048 a 056 | 009     | Nosso Numero                             | Conforme exemplo: AA2XXXXXX, onde XXXXXX deve ser um número sequêncial único    |
| 057 a 062 | 006     | Filer                                    | Deixar em branco                                                                |
| 063 a 070 | 008     | Data instrução                           | AAAAMMDD                                                                        |
| 071 a 071 | 001     | Vazio                                    |                                                                                 |
| 072 a 072 | 001     | Postagem                                 | "N"                                                                             |
| 073 a 073 | 001     | Filer                                    | Deixar em branco                                                                |
| 074 a 074 | 001     | Emissão boleto                           | "B"                                                                             |
| 075 a 076 | 002     | Vazio                                    |                                                                                 |
| 077 a 078 | 002     | Vazio                                    |                                                                                 |
| 079 a 082 | 004     | Filer                                    | Deixar em branco                                                                |
| 083 a 092 | 010     | Valor de desconto                        | Alinhado a direita e zeros à esquerda ou senão preencher com zeros              |
| 093 a 096 | 004     | % multa pagamento em atraso              | Alinhado a direita e zeros à esquerda ou senão preencher com zeros              |
| 097 a 108 | 012     | Filer                                    | Deixar em branco                                                                |
| 109 a 110 | 002     | Instrução                                | "01"- Cadastro de título                                                        |
| 111 a 120 | 010     | Seu número                               | Nunca pode repetir, se diferente de branco. Normalmente usado como numero da NF |
| 121 a 126 | 006     | Data de vencimento                       | DDMMAA (sete dias maior que a emissão)                                          |
| 127 a 139 | 013     | Valor                                    | Alinhado a direita e zeros à esquerda                                           |
| 140 a 148 | 009     | Filer                                    | Deixar em branco                                                                |
| 149 a 149 | 001     | Espécie                                  | "O"                                                                             |
| 150 a 150 | 001     | Aceite                                   | "S"                                                                             |
| 151 a 156 | 006     | Data de emissão                          | DDMMAA                                                                          |
| 157 a 158 | 002     | Protesto                                 | "00"- não protestar                                                             |
| 159 a 160 | 002     | Numero de dias protesto                  | Minimo 3 dias ou zeros para não protesto                                        |
| 161 a 173 | 012     | Valor de juros por dia de atraso         | Alinhado a direita e zeros à esquerda ou senão preencher com zeros              |
| 174 a 179 | 006     | Data limite de desconto                  | DDMMAA ou preencher com zeros                                                   |
| 180 a 192 | 013     | Zeros                                    |                                                                                 |
| 193 a 205 | 013     | Filer                                    | Deixar em branco                                                                |
| 206 a 218 | 013     | Zeros                                    |                                                                                 |
| 219 a 219 | 001     | Tipo de pessoa do pagador PF ou PJ       | "1"- PF; "2" - PJ                                                               |
| 220 a 220 | 001     | Filer                                    | Deixar em branco                                                                |
| 221 a 234 | 014     | CNPJ ou CPF do pagador                   | Alinhado com zeros a esquerda                                                   |
| 235 a 274 | 040     | Nome do pagador                          | Sem acentuação ou caracteres especiais                                          |
| 275 a 314 | 040     | Endereço do pagador                      | Sem acentuação ou caracteres especiais                                          |
| 315 a 319 | 005     | Zeros                                    | Preencher com zeros                                                             |
| 320 a 235 | 006     | Filer                                    | Deixar em branco                                                                |
| 326 a 326 | 001     | Filer                                    | Preencher com zeros                                                             |
| 327 a 224 | 008     | CEP do pagador                           |                                                                                 |
| 335 a 339 | 005     | Zeros                                    | Preencher com zeros                                                             |
| 340 a 353 | 014     | Zeros                                    | Preencher com zeros                                                             |
| 354 a 394 | 041     | Deixar em branco                         | Deixar em branco                                                                |
| 395 a 400 | 006     | Numero sequêncial do registro            | Primero registro deve começar com "000002"                                      |

## Registro *trailer* 

| Posição   | Tamanho | Descrição                            | Conteúdo                              |
|-----------|---------|--------------------------------------|---------------------------------------|
| 001 a 001 | 001     | Identificação do registro *trailer*  | "9"                                   |
| 002 a 002 | 001     | Identificação do arquivo de mressa   | "1"                                   |
| 003 a 005 | 003     | Numero Eximia                        | "987"                                 |
| 006 a 010 | 005     | Codigo do beneficiario               | Conta corrente sem o DV               |
| 011 a 394 | 384     | Filer                                | Deixar em branco                      |
| 395 a 400 | 006     | Numero sequencial do registro        | Alinhado à direita e zeros à esquerda |