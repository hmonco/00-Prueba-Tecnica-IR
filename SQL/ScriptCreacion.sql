CREATE DATABASE IF NOT EXISTS StudentReg;
USE StudentReg;

CREATE TABLE IF NOT EXISTS usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Tipo ENUM('Estudiante', 'Profesor') NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    FechaAlta DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaBaja DATETIME DEFAULT NULL,
    Clave VARCHAR(255) NOT NULL
);

CREATE TABLE Estudiantes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    UsuarioId INT NOT NULL,
    NumeroDocumento VARCHAR(50) NOT NULL,
    ProgramaCreditosId INT NOT NULL,
    CONSTRAINT FK_Estudiantes_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Estudiantes_ProgramaCreditos FOREIGN KEY (ProgramaCreditosId) REFERENCES ProgramaCreditos(Id)
);

CREATE TABLE ProgramaCreditos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Creditos INT NOT NULL DEFAULT 9
);

CREATE TABLE Profesores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    UsuarioId INT NOT NULL,
    CONSTRAINT FK_Profesores_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

CREATE TABLE Materias (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Creditos INT NOT NULL,
    ProfesorId INT NOT NULL,
    CONSTRAINT FK_Materias_Profesores FOREIGN KEY (ProfesorId) REFERENCES Profesores(Id),
    CONSTRAINT UQ_Materias_Nombre_Profesor UNIQUE (Nombre, ProfesorId)
);

CREATE TABLE EstudianteMaterias (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EstudianteId INT NOT NULL,
    MateriaId INT NOT NULL,
    CONSTRAINT FK_EstudianteMaterias_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id),
    CONSTRAINT FK_EstudianteMaterias_Materia FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);