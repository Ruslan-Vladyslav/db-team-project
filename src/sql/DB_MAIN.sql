-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema OpenDataModel
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `OpenDataModel` ;

-- -----------------------------------------------------
-- Schema OpenDataModel
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `OpenDataModel` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_520_ci ;
USE `OpenDataModel` ;

-- -----------------------------------------------------\
-- Table `OpenDataModel`.`Categoty`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Categoty` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Categoty` (
  `category_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `parent_category_id` INT NULL,
  PRIMARY KEY (`category_id`),
  INDEX `parent_category_idx` (`parent_category_id` ASC) INVISIBLE,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE,
  CONSTRAINT `parent_category`
    FOREIGN KEY (`parent_category_id`)
    REFERENCES `OpenDataModel`.`Categoty` (`category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Tag` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Tag` (
  `tag_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`tag_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Data`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Data` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Data` (
  `data_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `description` LONGTEXT NULL,
  `format` VARCHAR(45) NOT NULL,
  `content` VARCHAR(45) NOT NULL,
  `createdAt` DATETIME NOT NULL,
  `updatedAt` DATETIME NOT NULL,
  `category_id` INT NOT NULL,
-- PRIMARY KEY (`data_id`, `category_id`),
  PRIMARY KEY (`data_id`),
  INDEX `fk_Data_Categoty_idx` (`category_id` ASC) VISIBLE,
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE,
  CONSTRAINT `fk_Data_Categoty`
    FOREIGN KEY (`category_id`)
    REFERENCES `OpenDataModel`.`Categoty` (`category_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Link`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Link` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Link` (
  `link_id` INT NOT NULL AUTO_INCREMENT,
  `data_id` INT NOT NULL,
  `tag_id` INT NOT NULL,
  PRIMARY KEY (`link_id`, `data_id`, `tag_id`),
  INDEX `fk_Link_Data_idx` (`data_id` ASC) VISIBLE,
  INDEX `fk_Link_Tag_idx` (`tag_id` ASC) VISIBLE,
  CONSTRAINT `fk_Link_Data`
    FOREIGN KEY (`data_id`)
    REFERENCES `OpenDataModel`.`Data` (`data_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Link_Tag`
    FOREIGN KEY (`tag_id`)
    REFERENCES `OpenDataModel`.`Tag` (`tag_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Role` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Role` (
  `role_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`role_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`User` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`User` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `firstname` VARCHAR(45) NOT NULL,
  `lastname` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `login` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OpenDataModel`.`Access`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `OpenDataModel`.`Access` ;

CREATE TABLE IF NOT EXISTS `OpenDataModel`.`Access` (
  `access_id` INT NOT NULL AUTO_INCREMENT,
  `data_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  PRIMARY KEY (`access_id`, `data_id`, `user_id`, `role_id`),
  INDEX `fk_Access_Data_idx` (`data_id` ASC) VISIBLE,
  INDEX `fk_Access_Role_idx` (`role_id` ASC) VISIBLE,
  INDEX `fk_Access_User_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_Access_Data`
    FOREIGN KEY (`data_id`)
    REFERENCES `OpenDataModel`.`Data` (`data_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Access_Role`
    FOREIGN KEY (`role_id`)
    REFERENCES `OpenDataModel`.`Role` (`role_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Access_User`
    FOREIGN KEY (`user_id`)
    REFERENCES `OpenDataModel`.`User` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


