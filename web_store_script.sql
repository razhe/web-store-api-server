USE [web_store]
GO
/****** Object:  Table [dbo].[customer_address]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[customers]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] UNIQUE NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NULL,
	[phone] [nvarchar](50) NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deliveries]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[delivery_type]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[oauth_clients]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oauth_clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_name] [nvarchar](150) NOT NULL,
	[client_id] [nvarchar](max) NOT NULL,
	[client_secret] [nvarchar](max) NULL,
	[redirect_uri] [nvarchar](500) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_oauth_providers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[offers]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[orders]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[password_resets]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[post_gallery]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post_gallery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[post_id] [uniqueidentifier] NOT NULL,
	[file_name] [nvarchar](max) NOT NULL,
	[file_path] [nvarchar](max) NOT NULL,
	[content_type] [nvarchar](255) NOT NULL,
	[length] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_post_gallery] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post_types]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_post_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts]    Script Date: 17-03-2024 20:13:10 ******/
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
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_brands]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_brands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_brands] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_categories]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_gallery]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_gallery](
	[id] [int] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[file_name] [nvarchar](max) NOT NULL,
	[file_path] [nvarchar](max) NOT NULL,
	[content_type] [nvarchar](255) NOT NULL,
	[length] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_product_gallery] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_offers]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[product_sales]    Script Date: 17-03-2024 20:13:10 ******/
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
/****** Object:  Table [dbo].[product_subcategories]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_subcategories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NOT NULL,
	[name] [varchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_product_subcategories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 17-03-2024 20:13:10 ******/
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
	[price] [bigint] NOT NULL,
	[stock] [int] NOT NULL,
	[sku] [nvarchar](255) NOT NULL,
	[slug] [nvarchar](max) NOT NULL,
	[tags] [nvarchar](max) NULL,
	[active] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sales]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sales](
	[id] [uniqueidentifier] NOT NULL,
	[order_id] [uniqueidentifier] UNIQUE NOT NULL,
	[total] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[virified_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_sales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_oauth_client_requests]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_oauth_client_requests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[access_token] [nvarchar](max) NOT NULL,
	[refresh_token] [nvarchar](max) NOT NULL,
	[isActive]  AS (case when [expire_on]<sysdatetimeoffset() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end),
	[expire_on] [datetimeoffset](7) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_user_oauth_request] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17-03-2024 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [uniqueidentifier] NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NULL,
	[account_origin] [int] NOT NULL,
	[external_account_code] [int] NULL,
	[role] [int] NOT NULL,
	[active] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[customer_address] ADD  CONSTRAINT [DF_customer_address_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[customers] ADD  CONSTRAINT [DF_customers_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[deliveries] ADD  CONSTRAINT [DF_deliveries_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[delivery_type] ADD  CONSTRAINT [DF_delivery_type_name]  DEFAULT (sysdatetimeoffset()) FOR [name]
GO
ALTER TABLE [dbo].[oauth_clients] ADD  CONSTRAINT [DF_oauth_clients_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[offers] ADD  CONSTRAINT [DF_offers_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[password_resets] ADD  CONSTRAINT [DF_password_resets_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[post_gallery] ADD  CONSTRAINT [DF_post_gallery_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[post_types] ADD  CONSTRAINT [DF_post_types_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[post_types] ADD  CONSTRAINT [DF_post_types_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[posts] ADD  CONSTRAINT [DF_posts_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[posts] ADD  CONSTRAINT [DF_posts_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_brands] ADD  CONSTRAINT [DF_product_brands_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[product_brands] ADD  CONSTRAINT [DF_product_brands_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_categories] ADD  CONSTRAINT [DF_product_categories_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[product_categories] ADD  CONSTRAINT [DF_product_categories_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_gallery] ADD  CONSTRAINT [DF_product_gallery_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[product_subcategories] ADD  CONSTRAINT [DF_product_subcategories_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[product_subcategories] ADD  CONSTRAINT [DF_product_subcategories_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_products_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_products_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[sales] ADD  CONSTRAINT [DF_sales_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[user_oauth_client_requests] ADD  CONSTRAINT [DF_user_oauth_client_requests_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_created_at]  DEFAULT (sysdatetimeoffset()) FOR [created_at]
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
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_orders]
GO
ALTER TABLE [dbo].[user_oauth_client_requests]  WITH CHECK ADD  CONSTRAINT [FK_user_oauth_request_oauth_clients] FOREIGN KEY([client_id])
REFERENCES [dbo].[oauth_clients] ([id])
GO
ALTER TABLE [dbo].[user_oauth_client_requests] CHECK CONSTRAINT [FK_user_oauth_request_oauth_clients]
GO
ALTER TABLE [dbo].[user_oauth_client_requests]  WITH CHECK ADD  CONSTRAINT [FK_user_oauth_request_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[user_oauth_client_requests] CHECK CONSTRAINT [FK_user_oauth_request_users]
GO
