# 📜 Guía de Desarrollo - eVote360 Pro (Adrián)

Este archivo contiene el contexto crítico para que cualquier asistente de IA continúe el desarrollo del proyecto respetando las decisiones tomadas por Adrián.

## 👤 Perfil del Desarrollador: Adrián
- **Responsabilidad:** Módulos del Dirigente (Candidatos, Alianzas, Asignación de Puestos, Dashboard).
- **Estilo:** Pragmático, Senior, prefiere código limpio y funcional sobre teoría estricta.

## 🏗️ Estado de los Módulos
1. **Candidates (COMPLETO):** Entidad `Candidates`, Value Objects `FullName` y `CandidatePhoto`. Arquitectura CQRS con Handlers. Seguridad Multi-tenant por `PartyId`. Compila al 100%.
2. **LeaderDashboard (COMPLETO):** Query Handler con `ValidationResult<T>`. Consultas reales a candidatos y Mocks para el resto.
3. **PoliticalAlliances (EN PROCESO):** Entidad y Validator creados. Pendiente Handlers e Infraestructura.

## 🛠️ Reglas Técnicas Obligatorias
- **Arquitectura:** Onion (Cebolla) + CQRS.
- **Entidades:** Deben ser clases anémicas (planas).
- **Validación:** Usar Value Objects con `throw` para validación atómica y `ValidationResult` en Handlers.
- **Namespaces:** Evitar ambigüedades. Usar `Candidates` (plural) para la clase entidad.
- **Estilo de Handlers:** Usar lista privada `_errors` y bloques `try-catch` que retornen `ValidationResult.Failure(_errors)` (Estilo Joel).
- **DTOs:** Usar `get; set;` (NO usar `init` en DTOs).

## 🚀 Próximos Pasos
- Continuar con la implementación de los Handlers para el módulo de **Alianzas Políticas**.
