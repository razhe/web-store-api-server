USE [master]
GO
/****** Object:  Database [web_store]    Script Date: 27-12-2023 12:40:35 ******/
CREATE DATABASE [web_store]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'web_store', FILENAME = N'D:\Dev\DataBases\MSSQL15.MSSQLSERVER\MSSQL\DATA\web_store.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'web_store_log', FILENAME = N'D:\Dev\DataBases\MSSQL15.MSSQLSERVER\MSSQL\DATA\web_store_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [web_store] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [web_store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [web_store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [web_store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [web_store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [web_store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [web_store] SET ARITHABORT OFF 
GO
ALTER DATABASE [web_store] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [web_store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [web_store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [web_store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [web_store] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [web_store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [web_store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [web_store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [web_store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [web_store] SET  DISABLE_BROKER 
GO
ALTER DATABASE [web_store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [web_store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [web_store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [web_store] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [web_store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [web_store] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [web_store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [web_store] SET RECOVERY FULL 
GO
ALTER DATABASE [web_store] SET  MULTI_USER 
GO
ALTER DATABASE [web_store] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [web_store] SET DB_CHAINING OFF 
GO
ALTER DATABASE [web_store] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [web_store] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [web_store] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [web_store] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'web_store', N'ON'
GO
ALTER DATABASE [web_store] SET QUERY_STORE = OFF
GO
USE [web_store]
GO
/****** Object:  Table [dbo].[auth_clients]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auth_clients](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[secret_key] [nvarchar](255) NOT NULL,
	[url] [nvarchar](max) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_auth_clients_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[auth_requests]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auth_requests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[access_token] [nvarchar](max) NOT NULL,
	[refresh_token] [nvarchar](max) NOT NULL,
	[expire_on] [datetimeoffset](7) NOT NULL,
	[created_At] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_auth_requests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[coupons]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[coupons](
	[id] [uniqueidentifier] NOT NULL,
	[code] [nvarchar](150) NOT NULL,
	[discount] [int] NOT NULL,
	[expire_on] [datetimeoffset](7) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_coupons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer_address]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer_address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](500) NOT NULL,
	[number] [int] NOT NULL,
	[aditional_info] [nvarchar](500) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_customer_address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deliveries]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deliveries](
	[id] [uniqueidentifier] NOT NULL,
	[address_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[order_id] [uniqueidentifier] NOT NULL,
	[estimated_delivery_date] [datetimeoffset](7) NOT NULL,
	[carrier] [nvarchar](255) NULL,
	[tracking_number] [nvarchar](255) NULL,
	[shipping_cost] [bigint] NOT NULL,
	[status] [int] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_deliveries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delivery_type]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delivery_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_delivery_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[offers]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[offers](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[discount] [int] NOT NULL,
	[expire_on] [datetimeoffset](7) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_offers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [uniqueidentifier] NOT NULL,
	[customer_id] [uniqueidentifier] NOT NULL,
	[order_number] [nvarchar](255) NOT NULL,
	[status] [int] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[password_resets]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[password_resets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[code] [nvarchar](6) NOT NULL,
	[expire_on] [datetimeoffset](7) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_password_resets] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post_gallery]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post_gallery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[post_id] [uniqueidentifier] NOT NULL,
	[file_name] [nvarchar](max) NOT NULL,
	[file_path] [nvarchar](max) NOT NULL,
	[mime_type] [nvarchar](255) NOT NULL,
	[length] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_post_gallery] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post_types]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_post_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[type_id] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_brands]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_brands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_brands] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_categories]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_gallery]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_gallery](
	[id] [int] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[file_name] [nvarchar](max) NOT NULL,
	[file_path] [nvarchar](max) NOT NULL,
	[mime_type] [nvarchar](255) NOT NULL,
	[length] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_product_gallery] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_offers]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_offers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[offer_id] [uniqueidentifier] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[offer_price] [bigint] NOT NULL,
 CONSTRAINT [PK_product_offers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_sales]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_sales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[sale_id] [uniqueidentifier] NOT NULL,
	[quantity] [int] NOT NULL,
	[subtotal] [bigint] NOT NULL,
	[unit_price] [bigint] NOT NULL,
 CONSTRAINT [PK_product_sales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_subcategories]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_subcategories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NOT NULL,
	[name] [varchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_subcategories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [uniqueidentifier] NOT NULL,
	[subcategory_id] [int] NOT NULL,
	[brand_id] [int] NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[price] [bigint] NULL,
	[stock] [int] NOT NULL,
	[sku] [nvarchar](255) NOT NULL,
	[slug] [nvarchar](max) NOT NULL,
	[tags] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sales]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sales](
	[id] [uniqueidentifier] NOT NULL,
	[order_id] [uniqueidentifier] NOT NULL,
	[coupon_id] [uniqueidentifier] NULL,
	[subtotal] [bigint] NOT NULL,
	[total] [bigint] NOT NULL,
	[status] [int] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[virified_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_sales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_roles]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_roles](
	[id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_user_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 27-12-2023 12:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [uniqueidentifier] NOT NULL,
	[role_id] [int] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[auth_requests]  WITH CHECK ADD  CONSTRAINT [FK_auth_requests_auth_clients] FOREIGN KEY([client_id])
REFERENCES [dbo].[auth_clients] ([id])
GO
ALTER TABLE [dbo].[auth_requests] CHECK CONSTRAINT [FK_auth_requests_auth_clients]
GO
ALTER TABLE [dbo].[auth_requests]  WITH CHECK ADD  CONSTRAINT [FK_auth_requests_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[auth_requests] CHECK CONSTRAINT [FK_auth_requests_users]
GO
ALTER TABLE [dbo].[customer_address]  WITH CHECK ADD  CONSTRAINT [FK_customer_address_customers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[customer_address] CHECK CONSTRAINT [FK_customer_address_customers]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_users]
GO
ALTER TABLE [dbo].[deliveries]  WITH CHECK ADD  CONSTRAINT [FK_deliveries_customer_address] FOREIGN KEY([address_id])
REFERENCES [dbo].[customer_address] ([id])
GO
ALTER TABLE [dbo].[deliveries] CHECK CONSTRAINT [FK_deliveries_customer_address]
GO
ALTER TABLE [dbo].[deliveries]  WITH CHECK ADD  CONSTRAINT [FK_deliveries_delivery_type] FOREIGN KEY([type_id])
REFERENCES [dbo].[delivery_type] ([id])
GO
ALTER TABLE [dbo].[deliveries] CHECK CONSTRAINT [FK_deliveries_delivery_type]
GO
ALTER TABLE [dbo].[deliveries]  WITH CHECK ADD  CONSTRAINT [FK_deliveries_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[deliveries] CHECK CONSTRAINT [FK_deliveries_orders]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[password_resets]  WITH CHECK ADD  CONSTRAINT [FK_password_resets_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[password_resets] CHECK CONSTRAINT [FK_password_resets_users]
GO
ALTER TABLE [dbo].[post_gallery]  WITH CHECK ADD  CONSTRAINT [FK_post_gallery_posts] FOREIGN KEY([post_id])
REFERENCES [dbo].[posts] ([id])
GO
ALTER TABLE [dbo].[post_gallery] CHECK CONSTRAINT [FK_post_gallery_posts]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK_posts_post_types] FOREIGN KEY([type_id])
REFERENCES [dbo].[post_types] ([id])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK_posts_post_types]
GO
ALTER TABLE [dbo].[posts]  WITH CHECK ADD  CONSTRAINT [FK_posts_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[posts] CHECK CONSTRAINT [FK_posts_users]
GO
ALTER TABLE [dbo].[product_gallery]  WITH CHECK ADD  CONSTRAINT [FK_product_gallery_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_gallery] CHECK CONSTRAINT [FK_product_gallery_products]
GO
ALTER TABLE [dbo].[product_offers]  WITH CHECK ADD  CONSTRAINT [FK_product_offers_offers] FOREIGN KEY([offer_id])
REFERENCES [dbo].[offers] ([id])
GO
ALTER TABLE [dbo].[product_offers] CHECK CONSTRAINT [FK_product_offers_offers]
GO
ALTER TABLE [dbo].[product_offers]  WITH CHECK ADD  CONSTRAINT [FK_product_offers_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_offers] CHECK CONSTRAINT [FK_product_offers_products]
GO
ALTER TABLE [dbo].[product_sales]  WITH CHECK ADD  CONSTRAINT [FK_product_sales_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_sales] CHECK CONSTRAINT [FK_product_sales_products]
GO
ALTER TABLE [dbo].[product_sales]  WITH CHECK ADD  CONSTRAINT [FK_product_sales_sales] FOREIGN KEY([sale_id])
REFERENCES [dbo].[sales] ([id])
GO
ALTER TABLE [dbo].[product_sales] CHECK CONSTRAINT [FK_product_sales_sales]
GO
ALTER TABLE [dbo].[product_subcategories]  WITH CHECK ADD  CONSTRAINT [FK_product_subcategories_product_categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[product_categories] ([id])
GO
ALTER TABLE [dbo].[product_subcategories] CHECK CONSTRAINT [FK_product_subcategories_product_categories]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_product_brands] FOREIGN KEY([brand_id])
REFERENCES [dbo].[product_brands] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_product_brands]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_product_subcategories] FOREIGN KEY([subcategory_id])
REFERENCES [dbo].[product_subcategories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_product_subcategories]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_coupons] FOREIGN KEY([coupon_id])
REFERENCES [dbo].[coupons] ([id])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_coupons]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_orders]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_user_roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[user_roles] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_user_roles]
GO
USE [master]
GO
ALTER DATABASE [web_store] SET  READ_WRITE 
GO
