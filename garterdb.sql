-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema garterdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema garterdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `garterdb` DEFAULT CHARACTER SET utf8 ;
USE `garterdb` ;

-- -----------------------------------------------------
-- Table `garterdb`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `garterdb`.`Users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `fname` VARCHAR(45) NULL,
  `lname` VARCHAR(45) NULL,
  `alias` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `password` VARCHAR(255) NULL,
  `created_at` TIMESTAMP NULL DEFAULT NOW(),
  `updated_at` TIMESTAMP NULL DEFAULT NOW() ON UPDATE NOW(),
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `garterdb`.`Ideas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `garterdb`.`Ideas` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `itext` VARCHAR(255) NULL,
  `created_at` TIMESTAMP NULL,
  `updated_at` TIMESTAMP NULL,
  `userId` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Ideas_Users1_idx` (`userId` ASC),
  CONSTRAINT `fk_Ideas_Users1`
    FOREIGN KEY (`userId`)
    REFERENCES `garterdb`.`Users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `garterdb`.`Mediums`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `garterdb`.`Mediums` (
  `id` INT NULL,
  `ideaId` INT NOT NULL,
  `userId` INT NOT NULL,
  PRIMARY KEY (`ideaId`, `userId`),
  INDEX `fk_Ideas_has_Users_Users1_idx` (`userId` ASC),
  INDEX `fk_Ideas_has_Users_Ideas_idx` (`ideaId` ASC),
  CONSTRAINT `fk_Ideas_has_Users_Ideas`
    FOREIGN KEY (`ideaId`)
    REFERENCES `garterdb`.`Ideas` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ideas_has_Users_Users1`
    FOREIGN KEY (`userId`)
    REFERENCES `garterdb`.`Users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
