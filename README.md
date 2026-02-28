#University Workflow Automation – Monolithic Architecture

##Overview
This project presents a monolithic implementation of a university workflow automation system developed using ASP.NET Core (.NET 8). The system is designed as a baseline architecture for subsequent transformation into a microservice-based architecture as part of a research study on software architectural evolution in higher education systems.

##Purpose
The primary objective of this implementation is to:

Model core university workflow processes

Establish a functional monolithic baseline

Enable architectural comparison with a microservice-based design

Support academic research on scalability, modularity, and system evolution

Implemented Workflow Scenario

The system simulates a simplified academic process:

Student registration

Course creation

Course enrollment

Enrollment status approval/rejection

Audit logging of workflow events

Simulated notification handling upon approval

Architecture Characteristics

Single deployable ASP.NET Core Web API

Shared relational database (SQLite)

Modular internal structure (Domain, Infrastructure, Controllers)

Centralized business logic

Integrated audit logging mechanism

##Technologies Used

.NET 8

ASP.NET Core Web API

Entity Framework Core

SQLite

Swagger (OpenAPI)

Architectural Limitations (Intentional for Research Purposes)

Tight coupling between modules

Shared database schema

Single deployment unit

Internalized notification mechanism

These limitations serve as controlled design constraints for subsequent microservice decomposition and comparative architectural evaluation.

#Research Context

This implementation forms the baseline model for an academic study titled:

“Designing a Microservice-Based Software Architecture for University Workflow Automation”

The next phase of the research involves decomposing this monolithic system into independently deployable microservices and evaluating architectural trade-offs.
